using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using enterprisemvvm.Model;
using System.Data.SqlClient;
using LiteDB;

namespace enterprisemvvm.ViewModel
{
    class crudViewModel
    {


        public ObservableCollection<register> Registers
        {
            get
            {
                using (var db = new LiteDatabase(@"C:\Temp\MyData.db"))
                {
                    return new ObservableCollection<register>(db.GetCollection<register>("customers").FindAll());
                }
            }
        }

        /*
       public ICommand DeleteCommand { get; set; }
        private void InitializeCommands()
        {
            this.DeleteCommand = new DelegateCommand(OnDeleteCommandExecuted);
        }
        

        private void OnDeleteCommandExecuted(object obj)
        {
            using (var db = new LiteDatabase(@"C:\Temp\MyData.db"))
            {
                var args = obj as GridViewDeletingEventArgs;
                var customers = args.Items;
                foreach (var item in customers)
                {
                    var customer = item as register;
                    db.GetCollection<register>("customers").Delete(customer.Id);
                }
            }
        }
        */

        private void MakeSampleData()
        {
            using (var db = new LiteDatabase(@"C:\Temp\MyData.db"))
            {
                // Get a collection (or create, if doesn't exist)
                var col = db.GetCollection<register>("customers");

                if (col.Count() == 0)
                {
                    for (int i = 0; i < 20; i++)
                    {
                        var customer = new register
                        {
                            Name = "estevan",
                            Phone = "41 992599723",
                            Email = "estevan.louzada@gamil.com",
                            Password = "mypassword",
                            Cpf = 41651907803,
                            Address = "Alferes Poli 464"
                        };

                        col.Insert(customer);
                    }
                }
            }
        }


        private void InsertRegister(register novoregistro)
        {
            using (var db = new LiteDatabase(@"C:\Temp\MyData.db"))
            {
                // Get a collection (or create, if doesn't exist)
                var col = db.GetCollection<register>("customers");
                 
                 col.Insert(novoregistro);
                    
                
            }
        }


    }
}
