using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitOfWorkCaseStudy.Workflows.Api.Services
{
    public interface IPublishWorkflowService
    {
        void Publish(Guid id);
    }
}
