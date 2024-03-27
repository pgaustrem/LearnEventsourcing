using Microsoft.Extensions.Logging;
using NEventStore;

namespace Infrastructure
{

    public class NEventStoreRegistry
    {
        //setup NEventStore
        public static IStoreEvents SetupNEventStore()
        {
            var loggerFactory = LoggerFactory.Create(logging =>
            {
                logging
                    .SetMinimumLevel(LogLevel.Trace);                    ;
            });

            return Wireup.Init()
               .WithLoggerFactory(loggerFactory)
              //.UsingSqlPersistence("Name Of EventStore ConnectionString In Config File")
              .UsingInMemoryPersistence()
              .InitializeStorageEngine()                                                                     
              .Build();
        }
    }
}
