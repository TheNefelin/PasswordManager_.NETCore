using ClassLibrary.Models;
using ClassLibrary.Models.DTOs;
using ClassLibrary.Services.Interfaces;
using ClassLibrary.Services.Utils;
using Dapper;
using System.Data;

namespace ClassLibrary.Services.Services
{
    public class CoreService : ICoreService
    {
        private readonly IDbConnection _connection;
        private readonly PasswordUtil _passwordUtil;

        public CoreService(IDbConnection connection)
        {
            _connection = connection;
            _passwordUtil = new PasswordUtil();
        }

        public async Task<ResponseApi<CoreIVDTO>> RegisterAsync(CoreRequest<object> request, CancellationToken cancellationToken)
        {
            try
            {
                var authUser = await GetUser(request.IdUser, request.SqlToken);

                if (authUser == null)
                    return new ResponseApi<CoreIVDTO>
                    {
                        IsSucces = false,
                        StatusCode = 401,
                        Message = "Debes Iniciar Sesion."
                    };

                if (!string.IsNullOrEmpty(authUser.HashPM) && !string.IsNullOrEmpty(authUser.SaltPM))
                    return new ResponseApi<CoreIVDTO>
                    {
                        IsSucces = false,
                        StatusCode = 400,
                        Message = "Ya Tienes una Clave Creada."
                    };

                var (hash, salt) = _passwordUtil.HashPassword(request.Password);

                await _connection.ExecuteAsync(
                    $"UPDATE Auth_Users SET HashPM = @HashPM, SaltPM = @SaltPM WHERE Id = @IdUser AND SqlToken = @SqlToken",
                    new { HashPM = hash, SaltPM = salt, request.IdUser, request.SqlToken }
                );

                return new ResponseApi<CoreIVDTO>
                {
                    IsSucces = true,
                    StatusCode = 200,
                    Message = "Clave Registrada Correctamente",
                    Data = new CoreIVDTO() { IV = salt }
                };
            }
            catch (Exception ex)
            {
                return new ResponseApi<CoreIVDTO>
                {
                    IsSucces = true,
                    StatusCode = 500,
                    Message = "Error en la (RegisterAsync): " + ex.Message
                };
            }
        }

        public async Task<ResponseApi<CoreIVDTO>> GetIVAsync(CoreRequest<object> request, CancellationToken cancellationToken)
        {
            try
            {
                var authUser = await GetUser(request.IdUser, request.SqlToken);

                if (authUser == null)
                    return new ResponseApi<CoreIVDTO>
                    {
                        IsSucces = false,
                        StatusCode = 401,
                        Message = "Usuario o Contraseña Icorrecta."
                    };

                bool passwordCorrect = _passwordUtil.VerifyPassword(request.Password, authUser.HashPM, authUser.SaltPM);

                if (!passwordCorrect)
                    return new ResponseApi<CoreIVDTO>
                    {
                        IsSucces = false,
                        StatusCode = 401,
                        Message = "Usuario o Contraseña Icorrecta."
                    };

                return new ResponseApi<CoreIVDTO>
                {
                    IsSucces = true,
                    StatusCode = 200,
                    Message = "Autenticación Exitosa.",
                    Data = new CoreIVDTO() { IV = authUser.SaltPM }
                };
            }
            catch (Exception ex)
            {
                return new ResponseApi<CoreIVDTO>
                {
                    IsSucces = false,
                    StatusCode = 500,
                    Message = "Error en (LoginAsync): " + ex.Message
                };
            }
        }

        public async Task<ResponseApi<IEnumerable<CoreDTO>>> GetAllAsync(CoreRequest<object> request, CancellationToken cancellationToken)
        {
            try
            {
                var authUser = await GetUser(request.IdUser, request.SqlToken);

                if (authUser == null)
                    return new ResponseApi<IEnumerable<CoreDTO>>
                    {
                        IsSucces = false,
                        StatusCode = 401,
                        Message = "No Estas Autorizado."
                    };

                var core = await _connection.QueryAsync<CoreDTO>(
                    $"SELECT Id, Data01, Data02, Data03, IdUser FROM PM_Core WHERE IdUser = @IdUser",
                    new { request.IdUser }
                );

                return new ResponseApi<IEnumerable<CoreDTO>>
                {
                    IsSucces = true,
                    StatusCode = 200,
                    Message = "Ok",
                    Data = core
                };
            }
            catch (Exception ex)
            {
                return new ResponseApi<IEnumerable<CoreDTO>>
                {
                    IsSucces = false,
                    StatusCode = 500,
                    Message = "Error en (GetAllAsync): " + ex.Message
                };
            }
        }     

        public async Task<ResponseApi<CoreDTO>> InsertAsync(CoreRequest<CoreDTO> request, CancellationToken cancellationToken)
        {
            try
            {
                var authUser = await GetUser(request.IdUser, request.SqlToken);

                if (authUser == null)
                    return new ResponseApi<CoreDTO>
                    {
                        IsSucces = false,
                        StatusCode = 401,
                        Message = "No Estas Autorizado."
                    };

                var Id = await _connection.QueryFirstOrDefaultAsync<int>(
                    $"INSERT INTO PM_Core (Data01, Data02, Data03, IdUser) OUTPUT inserted.Id VALUES (@Data01, @Data02, @Data03, @IdUser)",
                    new { request.CoreData.Data01, request.CoreData.Data02, request.CoreData.Data03, request.IdUser }
                );

                request.CoreData.Id = Id;
                request.CoreData.IdUser = authUser.IdUser;

                return new ResponseApi<CoreDTO>
                {
                    IsSucces = true,
                    StatusCode = 200,
                    Message = "Ok",
                    Data = request.CoreData,
                };
            }
            catch (Exception ex)
            {
                return new ResponseApi<CoreDTO>
                {
                    IsSucces = false,
                    StatusCode = 500,
                    Message = "Error en la operación de base de datos: " + ex.Message
                };
            }
        }

        public async Task<ResponseApi<CoreDTO>> UpdateAsync(CoreRequest<CoreDTO> request, CancellationToken cancellationToken)
        {
            try
            {
                var authUser = await GetUser(request.IdUser, request.SqlToken);

                if (authUser == null)
                    return new ResponseApi<CoreDTO>
                    {
                        IsSucces = false,
                        StatusCode = 401,
                        Message = "No Estas Autorizado."
                    };

                var Id = await _connection.QueryFirstOrDefaultAsync<int>(
                    $"UPDATE PM_Core SET Data01 = @Data01, Data02 = @Data02, Data03 = @Data03 OUTPUT inserted.Id WHERE Id = @Id AND IdUser = @IdUser",
                    new { request.CoreData.Data01, request.CoreData.Data02, request.CoreData.Data03, request.CoreData.Id, request.IdUser }
                );

                request.CoreData.IdUser = authUser.IdUser;

                return new ResponseApi<CoreDTO>
                {
                    IsSucces = true,
                    StatusCode = 200,
                    Message = "Ok",
                    Data = request.CoreData,
                };
            }
            catch (Exception ex)
            {
                return new ResponseApi<CoreDTO>
                {
                    IsSucces = false,
                    StatusCode = 500,
                    Message = "Error en la operación de base de datos: " + ex.Message
                };
            }
        }

        public async Task<ResponseApi<object>> DeleteAsync(CoreRequest<CoreDTO> request, CancellationToken cancellationToken)
        {
            try
            {
                var authUser = await GetUser(request.IdUser, request.SqlToken);

                if (authUser == null)
                    return new ResponseApi<object>
                    {
                        IsSucces = false,
                        StatusCode = 401,
                        Message = "No Estas Autorizado."
                    };

                await _connection.ExecuteAsync(
                    $"DELETE FROM PM_Core WHERE Id = @Id AND IdUser = @IdUser",
                    new { request.CoreData.Id, request.IdUser }
                );

                return new ResponseApi<object>
                {
                    IsSucces = false,
                    StatusCode = 200,
                    Message = "Ok",
                };
            }
            catch (Exception ex)
            {
                return new ResponseApi<object>
                {
                    IsSucces = false,
                    StatusCode = 500,
                    Message = "Error en la operación de base de datos: " + ex.Message
                };
            }
        }

        private async Task<AuthUser?> GetUser(string IdUser, string SqlToken)
        {
            return await _connection.QueryFirstOrDefaultAsync<AuthUser>(
                $"SELECT a.Id AS IdUser, a.Email, a.HashLogin, a.SaltLogin, a.HashPM, a.SaltPM, a.SqlToken, b.Name AS Role FROM Auth_Users a INNER JOIN Auth_Profiles b ON a.IdProfile = b.Id WHERE a.Id = @IdUser AND a.SqlToken = @SqlToken",
                new { IdUser, SqlToken }
            );
        }
    }
}
