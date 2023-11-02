using System;
using api.Models;
using api.Utilities;
using Polly;
using Polly.CircuitBreaker;
using Polly.Retry;

namespace api.Repositories
{
    public interface IDocumentRepository
    {
        Task SyncDocumentsFromExternalSource(string email);
    }

    public class DocumentRepository : IDocumentRepository
    {
        private AsyncRetryPolicy retryPolicy;
        private AsyncCircuitBreakerPolicy _circuitBreakerPolicy;
        public DocumentRepository()
        {
            int MAX_RETRIES = 5;

            retryPolicy = Policy.Handle<Exception>(ex => ex.Message.ToString() == "Chaos created - sorry")
                .WaitAndRetryAsync(MAX_RETRIES, retryAttempt => {
                    var timeToWait = TimeSpan.FromSeconds(Math.Pow(2, retryAttempt));
                    Console.WriteLine($"Waiting {timeToWait.TotalSeconds} seconds");
                    return timeToWait;
                }
            );
        }
        public async Task SyncDocumentsFromExternalSource(string _)
        {
            // simulates random errors that occur with external services
            // leave this to emulate real life

            await retryPolicy.ExecuteAsync(() => {
                ChaosUtility.RollTheDice();
                return Task.CompletedTask;
            });


            // this simulates sending an email
            // leave this delay as 10s to emulate real life
           await Task.Delay(10000);
        }
    }
}

