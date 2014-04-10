using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitOfWorkCaseStudy.Workflows.Domain.Models
{
    public class WorkflowExceptions
    {
        public class MissingBeginStateException : Exception
        { }

        public class MissingEndStateException : Exception
        { }

        public class MultipleEndStatesException : Exception
        { }

        public class MissingFromStateException : Exception
        { }

        public class MissingToStateException : Exception
        { }

        public class IncorrectEndStateException : Exception
        { }

        public class MultipleBeginStatesException : Exception
        { }

        public class IncorrectBeginStateException : Exception
        { }

        public class OrphanedStateException : Exception
        { }

        public class FieldNeedsToBeRequiredException : Exception
        { }

        public class InfiniteCycleException : Exception
        { }
    }
}