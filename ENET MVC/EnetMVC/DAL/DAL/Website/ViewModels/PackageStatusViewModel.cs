using System.ComponentModel.DataAnnotations.Schema;

namespace Website.ViewModels
{
    public class PackageStatusViewModel
    {
        public int PackageStatusId { get; set; }

        public string TransitState { get; set; }

     
        public virtual PackageViewModel Package { get; set; }

    }
}