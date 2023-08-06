using DataAccess.Entities;

namespace NikoMedia.Models
{
    public class RenderConfigurationsViewModel
    {
        public List<Configuration> Configurations { get; set; }
        public int SelectedConfigurationId { get; set; }
        public string SendTo { get; set; }
    }

}
