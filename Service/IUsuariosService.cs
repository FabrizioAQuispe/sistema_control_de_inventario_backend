using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Tokens;
using SistemaControlDeInventario.Interface;
using SistemaControlDeInventario.Models;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SistemaControlDeInventario.Service
{
    public class UsuariosService : IUsuariosInterface
    {
        private readonly string _connectionString;
        private readonly JwtConfig _jwtConfig;

        public UsuariosService(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("SQL")
                ?? throw new InvalidOperationException("Connection string not found");
            _jwtConfig = new JwtConfig(configuration);
        }

        public async Task<List<PerfilDTO>> PerfilUsuario(LoginInputDTO login) =>
            await ExecuteQueryAsync(
                @"SELECT U.usuario_id, U.nombre_usuario, U.email, U.rol_id, R.nombre_rol, P.nombre_permiso 
                  FROM Usuarios U 
                  INNER JOIN Roles R ON U.rol_id = R.rol_id 
                  INNER JOIN Permisos P ON R.rol_id = P.rol_id 
                  WHERE U.email = @email AND U.passwod = @password",
                cmd => {
                    cmd.Parameters.AddWithValue("@email", login.email);
                    cmd.Parameters.AddWithValue("@password", login.password);
                },
                reader => new PerfilDTO
                {
                    usuario_id = reader.GetInt32("usuario_id"),
                    nombre_usuario = reader.GetString("nombre_usuario"),
                    email = reader.GetString("email"),
                    nombre_rol = reader.GetString("nombre_rol"),
                    nombre_permiso = reader.GetString("nombre_permiso"),
                    token = GenerateToken(login.email)
                });

        private async Task<List<T>> ExecuteQueryAsync<T>(string query, Action<SqlCommand> setupParams, Func<SqlDataReader, T> mapper)
        {
            using var connection = new SqlConnection(_connectionString);
            using var command = new SqlCommand(query, connection);
            setupParams(command);
            await connection.OpenAsync();
            using var reader = await command.ExecuteReaderAsync();
            var results = new List<T>();
            while (await reader.ReadAsync()) results.Add(mapper(reader));
            return results;
        }

        private string GenerateToken(string email) =>
            new JwtSecurityTokenHandler().WriteToken(new JwtSecurityToken(
                _jwtConfig.Issuer,
                _jwtConfig.Audience,
                new[] { new Claim(ClaimTypes.NameIdentifier, email) },
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtConfig.Key)),
                    SecurityAlgorithms.HmacSha256)));

        private record JwtConfig(string Key, string Issuer, string Audience)
        {
            public JwtConfig(IConfiguration config) : this(
                config["Jwt:Key"] ?? throw new InvalidOperationException("JWT Key not found"),
                config["Jwt:Issuer"] ?? throw new InvalidOperationException("JWT Issuer not found"),
                config["Jwt:Audience"] ?? throw new InvalidOperationException("JWT Audience not found"))
            { }
        }
    }
}