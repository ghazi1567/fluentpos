namespace FluentPOS.Modules.Invoicing.Core.Dtos
{
    public class DashboardDto
    {
        public int pending { get; set; }

        public int reQueued { get; set; }

        public int assignedToOutlet { get; set; }

        public int assignedToHeadOffice { get; set; }

        public int approved { get; set; }

        public int shipped { get; set; }

        public int preparing { get; set; }

        public int readyToShip { get; set; }

        public int verifying { get; set; }

        public int cityCorrection { get; set; }

        public int cancelled { get; set; }
    }

}
