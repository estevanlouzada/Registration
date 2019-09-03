using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;


namespace enterprisemvvm.Model
{
    class register :BaseNotifyPropertyChanged , ICloneable
    {
        private int _id;
        public int Id
        {
            get { return _id; }
            set { SetField(ref _id, value); }
        }
        private string _name;
        public string Name {
            get { return _name; }
            set { SetField(ref _name, value); }
        }
        private string _phone;
        public string Phone {
            get { return _phone; }
            set { SetField(ref _phone, value); }
        }
        private string _email;
        public string Email {
            get {return _email; }
            set {SetField(ref _email, value); }
        }
        private string _password;
        public string Password
        {
            get { return _password; }
            set { SetField(ref _password, value); }
        }
        private long _cpf;
        public long Cpf
        {
            get { return _cpf; }
            set { SetField(ref _cpf, value); }
        }
        private string _address;
        public string Address
        {
            get { return _address; }
            set { SetField(ref _address, value); }
        }

        public object ValidatorExtensions { get; private set; }

        public object Clone()
        {
            return this.MemberwiseClone();
        }


       


    }
}
