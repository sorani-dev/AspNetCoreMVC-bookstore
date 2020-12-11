using BookStoreMvc.Models;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStoreMvc.Repository
{
    public class MessageRepository : IMessageRepository
    {
        private IOptionsMonitor<NewBookAlertConfig> newBookAlertConfig;

        public MessageRepository(IOptionsMonitor<NewBookAlertConfig> newBookAlertConfig)
        {
            this.newBookAlertConfig = newBookAlertConfig;
            //newBookAlertConfig.OnChange(config => this.newBookAlertConfig = config);
        }

        public string GetName()
        {
            return newBookAlertConfig.CurrentValue.BookName;
        }
    }
}
