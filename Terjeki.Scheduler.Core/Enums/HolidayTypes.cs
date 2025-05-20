namespace Terjeki.Scheduler.Core
{
    public enum HolidayTypes
    {
        [Description("Nincs")]
        None,
        [Description("Szabadság")]
        Holiday,

        [Description("Betegszabadság")]
        SickLeave,

        [Description("Alapszabadság")]
        BasicLeave,

        [Description("Apasági szabadság")]
        PaternityLeave,

        [Description("Rendkívüli szabadság")]
        SpecialLeave
    }
}
