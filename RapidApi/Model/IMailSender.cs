namespace RapidApi.Model
{
    public interface IMailSender
    {
        public void SendMail(string mailAdress, string Key);
    }
}
