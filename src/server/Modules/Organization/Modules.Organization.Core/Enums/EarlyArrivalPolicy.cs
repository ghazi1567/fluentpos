namespace FluentPOS.Modules.Organization.Core.Enums
{
    public enum EarlyArrivalPolicy
    {
        /// <summary>
        /// ACTUAL TIME: THE SYSTEM CALCULATES THE WORKING HOURS OF AN EMPLOYEE AS SOON AS HE/SHE MARKS THEIR ATTENDANCE.
        /// </summary>
        ActualTime = 1,

        /// <summary>
        /// SHIFT TIME: THE SYSTEM CALCULATES THE WORKING HOURS OF AN EMPLOYEE AFTER HIS/HER SHIFT STARTS.
        /// </summary>
        ShiftTime = 2,
    }
}