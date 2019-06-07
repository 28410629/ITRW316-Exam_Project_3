using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ITRW316_Exam_Project_3.Startup))]
namespace ITRW316_Exam_Project_3
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
