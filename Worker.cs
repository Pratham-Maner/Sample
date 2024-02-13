using DatabaseSynchServiceUsingWorkerService.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Threading;
using System.Threading.Tasks;

namespace DatabaseSynchServiceUsingWorkerService
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IConfiguration _config;
        private readonly IServiceProvider _service;
        AppDbContext appDbContext;


        public Worker(ILogger<Worker> logger, IConfiguration config, IServiceProvider serviceProvider)
        {
            _logger = logger;
            _config = config;
            _service = serviceProvider;

        }


        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {


            while (!stoppingToken.IsCancellationRequested)
            {

                appDbContext = _service.CreateScope().ServiceProvider.GetService<AppDbContext>();
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);

                try
                {
                    await CheckAndSyncTablesAsync(stoppingToken);
                }
                catch (Exception ex)
                {
                    _logger.LogError($"An error occurred: {ex.Message}");
                }

                await Task.Delay(TimeSpan.FromSeconds(10), stoppingToken);
            }

        }
        private async Task CheckAndSyncTablesAsync(CancellationToken cancellationToken)
        {
            /*using (var connection = appDbContext.Database.GetDbConnection() as SqlConnection)
            {
                await connection.OpenAsync(cancellationToken);

                var commandText = "SELECT * FROM Table1";
                using (var command = new SqlCommand(commandText, connection))
                using (var reader = await command.ExecuteReaderAsync(cancellationToken))
                {
                    while (await reader.ReadAsync(cancellationToken))
                    {
                        var id = reader.GetInt32(reader.GetOrdinal("Id"));
                        var name = reader.GetString(reader.GetOrdinal("Name"));
                        var contact = reader.GetString(reader.GetOrdinal("Contact"));

                        // Apply the update to Table2
                        await UpdateTable2(id, name, contact, cancellationToken);
                    }
                }
            }*/

            //For table 1
            var tabl1 =  await appDbContext.Table1.ToListAsync();
            foreach (var tab in tabl1)
            {
                var id = tab.Id;
                var name = tab.Name;
                var contact = tab.Contact;

                // Apply the update to Table2
                await UpdateTable2(id, name, contact, cancellationToken);
            }
            
            //For table 2
            var tabl2 = await appDbContext.Table2.ToListAsync();
            foreach (var tab in tabl2)
            {
                var id = tab.Id;
                var name = tab.Name;
                var contact = tab.Contact;

                // Apply the update to Table2
                await UpdateTable2Delete(tab,id, name, contact, cancellationToken);
            }
        }

        private async Task UpdateTable2(int id, string name, string contact, CancellationToken cancellationToken)       
        {

            var existingEntry = await appDbContext.Table2.FirstOrDefaultAsync(e => e.Id == id);

            if (existingEntry == null)
            {
                // If entry does not exist in Table2, add it
                //await appDbContext.Table2.AddAsync(new Table2Entity { Name = name, Contact = contact }, cancellationToken);
                
               
                await appDbContext.Table2.AddAsync(new Table2Entity { Name = name, Contact = contact }, cancellationToken);
            }
            else
            {
                // If entry exists in Table2, update its data
                existingEntry.Name = name;
                existingEntry.Contact = contact;
            }

            await appDbContext.SaveChangesAsync();
        }

        private async Task UpdateTable2Delete(Table2Entity obj, int id, string name, string contact, CancellationToken cancellationToken)
        {

            var existingEntry = await appDbContext.Table1.FirstOrDefaultAsync(e => e.Id == id);

            if (existingEntry == null)
            {
                // If entry does not exist in Table2, add it
                //await appDbContext.Table2.AddAsync(new Table2Entity { Name = name, Contact = contact }, cancellationToken);


               appDbContext.Table2.Remove(obj);
            }
            

            await appDbContext.SaveChangesAsync();
        }


        public override Task StartAsync(CancellationToken cancellationToken)
        {
            //logger.LogInformation("Service Started");
            return base.StartAsync(cancellationToken);
        }
        public override Task StopAsync(CancellationToken cancellationToken)
        {
            //logger.LogInformation("Service Stoped");
            return base.StopAsync(cancellationToken);
        }


    }


}
