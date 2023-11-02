using api.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using api.Repositories;
using FluentValidation;

namespace api.Endpoint
{
    public static class ClientEndpoints
    {
        
        public static void ConfigureClientEndpoints(this WebApplication app)
        {

            app.MapGet("/client", GetAllClient)
                .WithName("GetAllClient").Produces<APIResponse>(200);

            app.MapPost("/client", CreateClient)
                .WithName("CreateClient")
                .Accepts<Client>("application/json")
                .Produces<APIResponse>(201)
                .Produces(400);

            app.MapPut("/client", UpdateClient)
                .WithName("UpdateClient")
                .Accepts<Client>("application/json")
                .Produces<APIResponse>(200)
                .Produces(400);

            app.MapGet("/client/{filtertext}", FilterClient)
                .WithName("FilterClient").Produces<APIResponse>(200);
        }

        private async static Task<IResult> GetAllClient(IClientRepository clientRepo)
        {
            APIResponse response = new();
            response.Result = await clientRepo.Get();
            response.IsSuccess = true;
            response.StatusCode = HttpStatusCode.OK;
            return Results.Ok(response);
        }
        // [Authorize]
        private async static Task<IResult> CreateClient(IClientRepository clientRepo, IEmailRepository emailRepository, 
            IDocumentRepository documentRepository, IValidator<Client> _validator, [FromBody] Client client)
        {
            APIResponse response = new() { IsSuccess = false, StatusCode = HttpStatusCode.BadRequest };
            
            try
            {
                var validationResult = await _validator.ValidateAsync(client);
                if (!validationResult.IsValid)
                {
                    return Results.ValidationProblem(validationResult.ToDictionary());
                }

                await clientRepo.Create(client);
                await emailRepository.Send(client.Email, "Hi there - welcome to my Carepatron portal.");
                await documentRepository.SyncDocumentsFromExternalSource(client.Email);
                
                response.Result = client;
                response.IsSuccess = true;
                response.StatusCode = HttpStatusCode.Created;
                return Results.Ok(response);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.StatusCode = HttpStatusCode.BadRequest;
                response.ErrorMessages.Add(ex.Message.ToString());
                return Results.BadRequest(response);
            }
        }
        private async static Task<IResult> UpdateClient(IClientRepository clientRepo, IEmailRepository emailRepository, 
            IDocumentRepository documentRepository, IValidator<Client> _validator, [FromBody] Client client)
        {
            APIResponse response = new() { IsSuccess = false, StatusCode = HttpStatusCode.BadRequest };
            try
            {
                var validationResult = await _validator.ValidateAsync(client);
                if (!validationResult.IsValid)
                {
                    return Results.ValidationProblem(validationResult.ToDictionary());
                }

                var findClient = await clientRepo.GetById(client.Id);
                if (findClient != null)
                {
                    var findClientEmail = findClient.Email;
                    await clientRepo.Update(client);
                    if (findClientEmail != client.Email)
                    {
                        await emailRepository.Send(client.Email, "Hi there - welcome to my Carepatron portal.");
                        await documentRepository.SyncDocumentsFromExternalSource(client.Email);
                    }
                }
                else
                {
                    throw new InvalidOperationException("Client is not found");
                }

                response.Result = client;
                response.IsSuccess = true;
                response.StatusCode = HttpStatusCode.OK;
                return Results.Ok(response);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.StatusCode = HttpStatusCode.BadRequest;
                response.ErrorMessages.Add(ex.Message.ToString());
                return Results.BadRequest(response);
            }
        }
        private async static Task<IResult> FilterClient(IClientRepository clientRepo, string filtertext)
        {
            APIResponse response = new();
            response.Result = await clientRepo.GetByText(filtertext);
            response.IsSuccess = true;
            response.StatusCode = HttpStatusCode.OK;
            return Results.Ok(response);
        }
    }
}
