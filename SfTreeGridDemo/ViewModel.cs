﻿using Syncfusion.UI.Xaml.TreeGrid;
using Syncfusion.UI.Xaml.Utility;
using Syncfusion.Windows.Shared;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace SelfRelationalCollection
{
    class ViewModel : INotifyPropertyChanged
    {
        private BaseCommand _BeginEditCommand;
        public BaseCommand BeginEditCommand
        {
            get
            {
                if (_BeginEditCommand == null)
                {
                    _BeginEditCommand = new BaseCommand(OnExecuteBeginEdit);
                }
                return _BeginEditCommand;
            }
        }

        private void OnExecuteBeginEdit(object obj)
        {
            //var args = obj as TreeGridCurrentCellBeginEditEventArgs;
            //var dataGrid = args.Column as SfTreeGrid;
            //var recordIndex = dataGrid.ResolveToRecordIndex(args.RowColumnIndex.RowIndex);
            //var record = dataGrid.View.GetRecordAt(recordIndex);
            //var data = record.Data as OrderInfo;
            //if (data != null && !data.IsEditable)
            //    args.Cancel = true;
        }

        private SolidColorBrush selectionBackground;

        public SolidColorBrush SelectionBackground
        {
            get { return selectionBackground; }
            set
            {
                selectionBackground = value;
                OnPropertyChanged("SelectionBackground");
            }
        }

        private SolidColorBrush selectionForeground;

        public SolidColorBrush SelectionForeground
        {
            get { return selectionForeground; }
            set
            {
                selectionForeground = value;
                OnPropertyChanged("SelectionForeground");
            }
        }

        private static Random random = new Random(123123);
        /// <summary>
        /// Initializes a new instance of the <see cref="ViewModel"/> class.
        /// </summary>
        public ViewModel()
        {
            this.PersonDetails = this.CreateGenericPersonData(2, 8);
        }

        private ObservableCollection<PersonInfo> _PersonDetails;

        /// <summary>
        /// Gets or sets the person details.
        /// </summary>
        /// <value>The person details.</value>
        public ObservableCollection<PersonInfo> PersonDetails
        {
            get { return _PersonDetails; }
            set { _PersonDetails = value; }
        }


        private ObservableCollection<PersonInfo1> _PersonDetails1;

        /// <summary>
        /// Gets or sets the person details.
        /// </summary>
        /// <value>The person details.</value>
        public ObservableCollection<PersonInfo1> PersonDetails1
        {
            get { return _PersonDetails1; }
            set { _PersonDetails1 = value; }
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="ViewModel"/> class.
        /// </summary>
        /// <param name="count">The count.</param>
        /// <param name="maxGenerations">The max generations.</param>
        public ViewModel(int count, int maxGenerations)
        {
            CreateGenericPersonData(count, maxGenerations);
        }

        /// <summary>
        /// Creates the child list.
        /// </summary>
        /// <param name="count">The count.</param>
        /// <param name="maxGenerations">The max generations.</param>
        /// <param name="lastName">The last name.</param>
        /// <returns></returns>
        public ObservableCollection<PersonInfo> CreateChildList(int count, int maxGenerations, string lastName)
        {
            ObservableCollection<PersonInfo> _children = new ObservableCollection<PersonInfo>();
            if (count > 0 && maxGenerations > 0)
            {
                _children = CreateGenericPersonData(count, maxGenerations - 1);
                foreach (PersonInfo p in _children)
                    p.LastName = lastName;
            }
            return _children;
        }

        public ObservableCollection<PersonInfo1> CreateChildList1(int count, int maxGenerations, string lastName)
        {
            ObservableCollection<PersonInfo1> _children = new ObservableCollection<PersonInfo1>();
            if (count > 0 && maxGenerations > 0)
            {
                //_children = CreateGenericPersonData(count, maxGenerations - 1);
                _children = CreateGenericPersonData1(count, maxGenerations - 1);
                //foreach (PersonInfo1 p in _children)
                //    p.LastName = lastName;
            }
            return _children;
        }

        /// <summary>
        /// Creates the generic person data.
        /// </summary>
        /// <param name="count">The count.</param>
        /// <param name="maxGenerations">The max generations.</param>
        /// <returns></returns>
        public ObservableCollection<PersonInfo> CreateGenericPersonData(int count, int maxGenerations)
        {
            var personList = new ObservableCollection<PersonInfo>();
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    int k = 0;
                    if (i % 2 == 0)
                        k = 2;
                    else if (i % 3 == 0)
                        k = 3;
                    else
                        k = 0;
                    var lastname = names2[k];
                    personList.Add(new PersonInfo()
                    {
                        FirstName = names1[k],
                        LastName = lastname,
                        Children = this.CreateChildList(count, (int)Math.Max(0, maxGenerations - 1), lastname),
                        MyEyeColor = color[k],
                        DOB = new DateTime(2000, 2, 2),
                    });
                    
                }
            }
            return personList;
        }
        public ObservableCollection<PersonInfo1> CreateGenericPersonData1(int count, int maxGenerations)
        {
            var personList = new ObservableCollection<PersonInfo1>();
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    var lastname = names2[random.Next(names2.GetLength(0))];
                    personList.Add(new PersonInfo1()
                    {
                        FirstName = names1[random.Next(names1.GetLength(0))],
                        LastName = lastname,
                        //Children = this.CreateChildList1(count, (int)Math.Max(0, maxGenerations - 1), lastname),
                        Children = this.CreateChildList1(3, (int)Math.Max(0, maxGenerations - 1), lastname),

                        MyEyeColor = color[random.Next(color.GetLength(0))],
                        //DOB = GenerateRandomDate()
                    });
                }
            }
            return personList;
        }
        /// <summary>
        /// Generates the random date.
        /// </summary>
        /// <returns></returns>
        private DateTime GenerateRandomDate()
        {
            int randInt = random.Next(4000);
            return DateTime.Now.AddDays(-8000 + randInt);
        }

        #region Array Collection

        private static string[] names1 = new string[]{
            "George","John","Thomas","James","Andrew","Martin","William","Zachary",
            "Millard","Franklin","Abraham","Ulysses","Rutherford","Chester","Grover","Benjamin",
            "Theodore","Woodrow","Warren","Calvin","Herbert","Franklin","Harry","Dwight","Lyndon",
            "Richard","Gerald","Jimmy","Ronald","George","Bill", "Barack", "Warner","Peter", "Larry"
        };
        private static string[] names2 = new string[]{
             "Washington","Adams","Jefferson","Madison","Monroe","Jackson","Van Buren","Harrison","Tyler",
             "Polk","Taylor","Fillmore","Pierce","Buchanan","Lincoln","Johnson","Grant","Hayes","Garfield",
             "Arthur","Cleveland","Harrison","McKinley","Roosevelt","Taft","Wilson","Harding","Coolidge",
             "Hoover","Truman","Eisenhower","Kennedy","Johnson","Nixon","Ford","Carter","Reagan","Bush",
             "Clinton","Bush","Obama","Smith","Jones","Stogner"
        };
        private static string[] color = new string[]{
            "Red", "Blue", "Brown", "Unknown"
        };


        #endregion

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string prop)
        {
            if(PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
            }
        }
    }
}
