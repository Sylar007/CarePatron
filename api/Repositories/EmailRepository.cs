using api.Data;
using api.Models;
using api.Utilities;
using Microsoft.EntityFrameworkCore;
using Polly;
using Polly.CircuitBreaker;
using Polly.Retry;

namespace api.Repositories
{
    public interface IEmailRepository
    {
        Task Send(string email, string message);
    }

    public class EmailRepository : IEmailRepository
    {
        private AsyncRetryPolicy retryPolicy;
        public EmailRepository()
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
        public async Task Send(string _, string __)
        {
            // simulates random errors that occur with external services
            // leave this to emulate real life
            await retryPolicy.ExecuteAsync(() => {
                ChaosUtility.RollTheDice();
                return Task.CompletedTask;
            });

            // simulates sending an email
            // leave this delay as 10s to emulate real life
            await Task.Delay(10000);
        }
    }
}

