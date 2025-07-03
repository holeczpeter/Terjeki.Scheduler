using Org.BouncyCastle.Asn1.Ocsp;
using Terjeki.Scheduler.Core.Entities;

namespace Terjeki.Scheduler.Core.Model.Email
{
    public static class Messages
    {
        public static StringBuilder BuildNewEventMessage(string currentDriver, CreateEventCommand request, Bus currentBus)
        {
            var htmlBody = new StringBuilder();

            htmlBody.AppendLine("<!DOCTYPE html>");
            htmlBody.AppendLine("<html lang=\"hu\">");
            htmlBody.AppendLine("<head><meta charset=\"UTF-8\"><style>");
            htmlBody.AppendLine("  body { font-family: Arial, sans-serif; color: #333; }");
            htmlBody.AppendLine("  .container { max-width: 600px; margin: auto; padding: 20px; }");
            htmlBody.AppendLine("  h2 { color: #005CAF; }");
            htmlBody.AppendLine("  table { width: 100%; border-collapse: collapse; margin-top: 20px; }");
            htmlBody.AppendLine("  th, td { padding: 10px; border-bottom: 1px solid #e0e0e0; text-align: left; }");
            htmlBody.AppendLine("  th { background-color: #f5f5f5; }");
            htmlBody.AppendLine("  .footer { margin-top: 30px; font-size: 0.9em; color: #777; }");
            htmlBody.AppendLine("</style></head>");
            htmlBody.AppendLine("<body><div class=\"container\">");
            htmlBody.AppendLine("  <h2>Utazás részletei</h2>");
            htmlBody.AppendLine($"  <p>Kedves <strong>{currentDriver}</strong>,</p>");
            htmlBody.AppendLine("  <p>Az alábbiakban láthatod az utazásod adatait:</p>");
            htmlBody.AppendLine("  <table>");
            htmlBody.AppendLine($"    <tr><th>Címzett</th><td>{currentDriver}</td></tr>");
            htmlBody.AppendLine($"    <tr><th>Indulás</th><td>{request.StartDate:yyyy-MM-dd HH:mm}</td></tr>");
            htmlBody.AppendLine($"    <tr><th>Érkezés</th><td>{request.EndDate:yyyy-MM-dd HH:mm}</td></tr>");
            htmlBody.AppendLine($"    <tr><th>Leírás</th><td>{request.Description}</td></tr>");
            htmlBody.AppendLine($"    <tr><th>Részletek</th><td>{request.Summary}</td></tr>");
            htmlBody.AppendLine($"    <tr><th>Busz rendszáma</th><td>{currentBus.LicensePlateNumber}</td></tr>");
            htmlBody.AppendLine("  </table>");
            htmlBody.AppendLine("  <p class=\"footer\">");
            htmlBody.AppendLine("    Ha bármilyen kérdésed van, kérlek, jelezd vissza.<br>");
            htmlBody.AppendLine("    Jó utat kívánunk!<br><em>A Te Csapatod</em>");
            htmlBody.AppendLine("  </p>");
            htmlBody.AppendLine("</div></body></html>");

            return htmlBody;
        }
        public static StringBuilder BuildUpdatedEventMessage(string currentDriver, CreateEventCommand request, Bus currentBus)
        {
            var htmlBody = new StringBuilder();

            htmlBody.AppendLine("<!DOCTYPE html>");
            htmlBody.AppendLine("<html lang=\"hu\">");
            htmlBody.AppendLine("<head><meta charset=\"UTF-8\"><style>");
            htmlBody.AppendLine("  body { font-family: Arial, sans-serif; color: #333; }");
            htmlBody.AppendLine("  .container { max-width: 600px; margin: auto; padding: 20px; }");
            htmlBody.AppendLine("  h2 { color: #005CAF; }");
            htmlBody.AppendLine("  table { width: 100%; border-collapse: collapse; margin-top: 20px; }");
            htmlBody.AppendLine("  th, td { padding: 10px; border-bottom: 1px solid #e0e0e0; text-align: left; }");
            htmlBody.AppendLine("  th { background-color: #f5f5f5; }");
            htmlBody.AppendLine("  .footer { margin-top: 30px; font-size: 0.9em; color: #777; }");
            htmlBody.AppendLine("</style></head>");
            htmlBody.AppendLine("<body><div class=\"container\">");
            htmlBody.AppendLine("  <h2>Utazás részletei</h2>");
            htmlBody.AppendLine($"  <p>Kedves <strong>{currentDriver}</strong>,</p>");
            htmlBody.AppendLine("   <p>Az utazásod részletei megváltoztak.Az alábbiakban láthatod az adatait:</p>");
            htmlBody.AppendLine("  <table>");
            htmlBody.AppendLine($"    <tr><th>Címzett</th><td>{currentDriver}</td></tr>");
            htmlBody.AppendLine($"    <tr><th>Indulás</th><td>{request.StartDate:yyyy-MM-dd HH:mm}</td></tr>");
            htmlBody.AppendLine($"    <tr><th>Érkezés</th><td>{request.EndDate:yyyy-MM-dd HH:mm}</td></tr>");
            htmlBody.AppendLine($"    <tr><th>Leírás</th><td>{request.Description}</td></tr>");
            htmlBody.AppendLine($"    <tr><th>Részletek</th><td>{request.Summary}</td></tr>");
            htmlBody.AppendLine($"    <tr><th>Busz rendszáma</th><td>{currentBus.LicensePlateNumber}</td></tr>");
            htmlBody.AppendLine("  </table>");
            htmlBody.AppendLine("  <p class=\"footer\">");
            htmlBody.AppendLine("    Ha bármilyen kérdésed van, kérlek, jelezd vissza.<br>");
            htmlBody.AppendLine("    Jó utat kívánunk!<br><em>A Te Csapatod</em>");
            htmlBody.AppendLine("  </p>");
            htmlBody.AppendLine("</div></body></html>");

            return htmlBody;
        }
        public static StringBuilder BuildServiceDowntimeMessage(
       string currentDriver,
       Bus currentBus,
       DateTime serviceStart,
       DateTime serviceEnd,
       string serviceDescription)
        {
            var htmlBody = new StringBuilder();

            htmlBody.AppendLine("<!DOCTYPE html>");
            htmlBody.AppendLine("<html lang=\"hu\">");
            htmlBody.AppendLine("<head><meta charset=\"UTF-8\"><style>");
            htmlBody.AppendLine("  body { font-family: Arial, sans-serif; color: #333; }");
            htmlBody.AppendLine("  .container { max-width: 600px; margin: auto; padding: 20px; }");
            htmlBody.AppendLine("  h2 { color: #005CAF; }");
            htmlBody.AppendLine("  table { width: 100%; border-collapse: collapse; margin-top: 20px; }");
            htmlBody.AppendLine("  th, td { padding: 10px; border-bottom: 1px solid #e0e0e0; text-align: left; }");
            htmlBody.AppendLine("  th { background-color: #f5f5f5; }");
            htmlBody.AppendLine("  .footer { margin-top: 30px; font-size: 0.9em; color: #777; }");
            htmlBody.AppendLine("</style></head>");
            htmlBody.AppendLine("<body><div class=\"container\">");
            htmlBody.AppendLine("  <h2>Busz karbantartás</h2>");
            htmlBody.AppendLine($"  <p>Kedves <strong>{currentDriver}</strong>,</p>");
            htmlBody.AppendLine("  <p>A következő busz karbantartásra kerül az alábbi időpontban:</p>");
            htmlBody.AppendLine("  <table>");
            htmlBody.AppendLine($"    <tr><th>Busz rendszáma</th><td>{currentBus.LicensePlateNumber}</td></tr>");
            htmlBody.AppendLine($"    <tr><th>Karbantartás kezdete</th><td>{serviceStart:yyyy-MM-dd HH:mm}</td></tr>");
            htmlBody.AppendLine($"    <tr><th>Karbantartás vége</th><td>{serviceEnd:yyyy-MM-dd HH:mm}</td></tr>");
            htmlBody.AppendLine($"    <tr><th>Leírás</th><td>{serviceDescription}</td></tr>");
            htmlBody.AppendLine("  </table>");
            htmlBody.AppendLine("  <p class=\"footer\">");
            htmlBody.AppendLine("    Amennyiben kérdésed van, kérlek jelezd felénk!<br>");
            htmlBody.AppendLine("    Üdvözlettel,<br><em>A Te Csapatod</em>");
            htmlBody.AppendLine("  </p>");
            htmlBody.AppendLine("</div></body></html>");

            return htmlBody;
        }
        public static StringBuilder BuildModifedServiceDowntimeMessage(
      string currentDriver,
      Bus currentBus,
      DateTime serviceStart,
      DateTime serviceEnd,
      string serviceDescription)
        {
            var htmlBody = new StringBuilder();

            htmlBody.AppendLine("<!DOCTYPE html>");
            htmlBody.AppendLine("<html lang=\"hu\">");
            htmlBody.AppendLine("<head><meta charset=\"UTF-8\"><style>");
            htmlBody.AppendLine("  body { font-family: Arial, sans-serif; color: #333; }");
            htmlBody.AppendLine("  .container { max-width: 600px; margin: auto; padding: 20px; }");
            htmlBody.AppendLine("  h2 { color: #005CAF; }");
            htmlBody.AppendLine("  table { width: 100%; border-collapse: collapse; margin-top: 20px; }");
            htmlBody.AppendLine("  th, td { padding: 10px; border-bottom: 1px solid #e0e0e0; text-align: left; }");
            htmlBody.AppendLine("  th { background-color: #f5f5f5; }");
            htmlBody.AppendLine("  .footer { margin-top: 30px; font-size: 0.9em; color: #777; }");
            htmlBody.AppendLine("</style></head>");
            htmlBody.AppendLine("<body><div class=\"container\">");
            htmlBody.AppendLine("  <h2>Busz karbantartás módosítása</h2>");
            htmlBody.AppendLine($"  <p>Kedves <strong>{currentDriver}</strong>,</p>");
            htmlBody.AppendLine("  <p>A következő busz karbantartásra kerül az alábbi időpontban:</p>");
            htmlBody.AppendLine("  <table>");
            htmlBody.AppendLine($"    <tr><th>Busz rendszáma</th><td>{currentBus.LicensePlateNumber}</td></tr>");
            htmlBody.AppendLine($"    <tr><th>Karbantartás kezdete</th><td>{serviceStart:yyyy-MM-dd HH:mm}</td></tr>");
            htmlBody.AppendLine($"    <tr><th>Karbantartás vége</th><td>{serviceEnd:yyyy-MM-dd HH:mm}</td></tr>");
            htmlBody.AppendLine($"    <tr><th>Leírás</th><td>{serviceDescription}</td></tr>");
            htmlBody.AppendLine("  </table>");
            htmlBody.AppendLine("  <p class=\"footer\">");
            htmlBody.AppendLine("    Amennyiben kérdésed van, kérlek jelezd felénk!<br>");
            htmlBody.AppendLine("    Üdvözlettel,<br><em>A Te Csapatod</em>");
            htmlBody.AppendLine("  </p>");
            htmlBody.AppendLine("</div></body></html>");

            return htmlBody;
        }
    }
}
