using Whoever.Entities.Common;

namespace BaseProject.Domain.Constants
{
    public abstract class RolesNames : Enumeration
    {
        public static RolesNames UserManager = new UserManagerType();
        public static RolesNames Operator= new OperatorType();

        protected RolesNames(int id, string name)
            : base(id, name)
        {
        }
       

        private class UserManagerType : RolesNames
        {
            public UserManagerType() : base(1, "User Manager")
            { }
        }


        private class OperatorType : RolesNames
        {
            public OperatorType() : base(2, "Operator")
            { }
        }

    }
   
}
