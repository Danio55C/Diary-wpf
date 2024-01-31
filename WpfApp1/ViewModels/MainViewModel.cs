using Diary.Commands;
using Diary.Models;
using Diary.Views;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Diary.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        public MainViewModel()
        {
            AddStudentCommand = new RelayCommand(AddEditStudent);
            EdditStudentCommand = new RelayCommand(AddEditStudent, CanEditDeleteStudent);
            DeleteStudentCommand = new AsyncRelayCommand(DeleteStudent, CanEditDeleteStudent);
            RefreshStudentsCommand = new RelayCommand(RefreshStudents);
            RefreshDiary();

            InitGroups();
        }




        public ICommand AddStudentCommand { get; set; }
        public ICommand EdditStudentCommand { get; set; }
        public ICommand DeleteStudentCommand { get; set; }
        public ICommand RefreshStudentsCommand { get; set; }

        private Student _selectedStudent;


        public Student SelectedStudent
        {
            get { return _selectedStudent; }
            set
            {
                _selectedStudent = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<Student> _students;

        public ObservableCollection<Student> Students
        {
            get { return _students; }
            set
            {
                _students = value;
                OnPropertyChanged();
            }
        }

        private int _selectedGroupId;

        public int SelectedGroupId
        {
            get { return _selectedGroupId; }
            set
            {
                _selectedGroupId = value;
                OnPropertyChanged();
            }
        }
        private ObservableCollection<Group> _group;
        public ObservableCollection<Group> Groups
        {
            get { return _group; }
            set
            {
                _group = value;
                OnPropertyChanged();
            }
        }

        private void RefreshStudents(object obj)
        {
            RefreshDiary();
        }

        private void InitGroups()
        {
            Groups = new ObservableCollection<Group>
            {
                new Group {Id=0,Name="Wszystkie"},
                new Group {Id=1,Name="1A"},
                new Group {Id=2,Name="2A"}
            };

            SelectedGroupId = 0;
        }

        private bool CanEditDeleteStudent(object obj)
        {
            return SelectedStudent != null;
        }

        private async Task DeleteStudent(object obj)
        {
            var metroWindow = Application.Current.MainWindow as MetroWindow;
            var dialog = await metroWindow.ShowMessageAsync("Usuwanie Ucznia", $"Czy napewno chcesz usunąć ucznia {SelectedStudent.FirstName} {SelectedStudent.LastName}?", MessageDialogStyle.AffirmativeAndNegative);

            if (dialog != MessageDialogResult.Affirmative)
                return;

            //usuwanie ucznai z bazy

            RefreshDiary();
        }



        private void AddEditStudent(object obj)
        {
            var addEditStudentWindow = new AddEdditStudentView(obj as Student);
            addEditStudentWindow.Closed += AddEdditStudentWindow_Closed;
            addEditStudentWindow.ShowDialog();
        }

        private void AddEdditStudentWindow_Closed(object sender, EventArgs e)
        {
            RefreshDiary();
        }
        private void RefreshDiary()
        {
            Students = new ObservableCollection<Student>
            {
                new Student
                {
                    FirstName="daniel",
                    LastName="janca",
                    Group=new Group {Id=1}
                },
                new Student
                {
                    FirstName="marek",
                    LastName="janasdsa",
                    Group=new Group {Id=2}
                },
                new Student
                {
                    FirstName="jan",
                    LastName="janca",
                    Group=new Group {Id=1}
                },
            };
        }




    }




}
