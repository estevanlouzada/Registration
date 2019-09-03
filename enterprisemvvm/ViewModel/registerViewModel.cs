using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiteDB;
using System.Collections.ObjectModel;
using enterprisemvvm.Model;
using System.Diagnostics;

namespace enterprisemvvm.ViewModel
{
    class registerViewModel:BaseNotifyPropertyChanged
    {
        //registers are the model of register things
        // make  a collection of register are the mirror of the database

        // public System.Collections.ObjectModel.ObservableCollection<Model.register> Registers { get; private set; }

        // make registers the data inside db
        
        public ObservableCollection<register> Registers { get; private set; }
       public ObservableCollection<register> dbRegister() {
           
                using (var db = new LiteDatabase(@"MyData.db"))
                {
                    return new ObservableCollection<register>(db.GetCollection<register>("register").FindAll());
                }
          
        }

        public void insertRegister(register NewRegister)
        {
            using (var db = new LiteDatabase(@"MyData.db"))
            {
                var col = db.GetCollection<register>("register");
                col.Insert(NewRegister);
            }
        }
        public void RemoveDB(register customer)
        {
            using (var db = new LiteDatabase(@"MyData.db"))
            {

             
                   db.GetCollection<register>("register").Delete(customer.Id);
             
            }
        }
        public void EditDB( register editable)
        {
            using (var db = new LiteDatabase(@"MyData.db"))
            {

                db.GetCollection<register>("register").Update(editable);
               

            }
        }

        private Model.register _registerSelected;

        // when not have any item to select the button delete are disable
        public Model.register RegisterSelected
        {
            get { return _registerSelected; }
            set
            {

                SetField(ref _registerSelected, value);
                Delete.RaiseCanExecuteChanged();
                Edit.RaiseCanExecuteChanged();
            }
        }
        public registerViewModel()
        {
            Registers = new System.Collections.ObjectModel.ObservableCollection<Model.register>();
            Registers = dbRegister();


            // Registers.Add(register);

            RegisterSelected = Registers.FirstOrDefault();   
        }

        public class DeleteCommand : BaseCommand
        {
            public override bool CanExecute(object parameter)
            {
                var viewModel = parameter as registerViewModel;
                return viewModel != null && viewModel.RegisterSelected != null;
            }

            public override void Execute(object parameter)
            {
                var viewModel = (registerViewModel)parameter;
                viewModel.RemoveDB(viewModel.RegisterSelected);
                viewModel.Registers.Remove(viewModel.RegisterSelected);
                viewModel.RegisterSelected = viewModel.Registers.FirstOrDefault();
            }
        }

        public DeleteCommand Delete { get; private set; } = new DeleteCommand();


        public class NovoCommand : BaseCommand
        {
            public override bool CanExecute(object parameter)
            {
                return parameter is registerViewModel;
            }

            public override void Execute(object parameter)
            {
                // my view is the other view 
                var viewModel = (registerViewModel)parameter;
                // define the data 
                var NewRegister = new register();
                // get the max id 
                 var maxId = 0;
                 if (viewModel.Registers.Any())
                 {
                     maxId = viewModel.Registers.Max(f => f.Id);
                 }
                 NewRegister.Id = maxId + 1;
                
                // var latest = coll.FindOne(Query.All(1));
                var fw = new registerWindow();
                fw.DataContext = NewRegister;
                fw.ShowDialog();

                if (fw.DialogResult.HasValue && fw.DialogResult.Value)
                {
                    // viewModel.Registers.Add(NewRegister);

                    viewModel.insertRegister(NewRegister);

                  

                    // var abelinha = (registerViewModel)parameter;
                    //viewModel.Registers = abelinha.refreshRegister();
                    viewModel.Registers.Add(NewRegister);
                    viewModel.RegisterSelected = NewRegister;
                }
            }
        }
        public NovoCommand Novo { get; private set; } = new NovoCommand();

        public class EditCommand : BaseCommand
        {
            public override bool CanExecute(object parameter)
            {
                var viewModel = parameter as registerViewModel;
                return viewModel != null && viewModel.RegisterSelected != null;
            }

            public override void Execute(object parameter)
            {
                var viewModel = (registerViewModel)parameter;
                var cloneregister = (Model.register)viewModel.RegisterSelected.Clone();
                var fw = new registerWindow();
                fw.DataContext = cloneregister;
                fw.ShowDialog();

                if (fw.DialogResult.HasValue && fw.DialogResult.Value)
                {
                    viewModel.RegisterSelected.Name = cloneregister.Name;
                    viewModel.RegisterSelected.Phone = cloneregister.Phone;
                    viewModel.RegisterSelected.Email = cloneregister.Email;
                    viewModel.RegisterSelected.Password = cloneregister.Password;
                    viewModel.RegisterSelected.Cpf = cloneregister.Cpf;
                    viewModel.RegisterSelected.Address= cloneregister.Address;

                    viewModel.EditDB(cloneregister);
                }
            }
        }
        public EditCommand Edit { get; private set; } = new EditCommand();

    }
}
