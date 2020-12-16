using BookStoreMvc.Models;
using Microsoft.Extensions.Options;

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
