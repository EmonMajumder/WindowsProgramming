using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercise_5
{
    class Data
    {
        private String firstname;
        private String lastname;
        private String birthdate;
        private String email;

        public String FirstName
        {
            get { return firstname; }
            set { firstname = value; }
        }

        public String LastName
        {
            get { return lastname; }
            set { lastname = value; }
        }

        public String BirthDate
        {
            get { return birthdate; }
            set { birthdate = value; }
        }

        public String Email
        {
            get { return email; }
            set { email = value; }
        }
    }
}
