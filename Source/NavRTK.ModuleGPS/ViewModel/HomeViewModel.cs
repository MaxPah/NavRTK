using Microsoft.Practices.Prism.Regions;
using NavRTK.ModuleGPS.Helper;
using NavRTK.ModuleGPS.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;

namespace NavRTK.ModuleGPS.ViewModel
{
    [Export(typeof(HomeViewModel))]
    public class HomeViewModel : INotifyPropertyChanged
    {
        [Import]
        public IRegionManager regionManager { private get; set; }

        #region FIELDS
        private Queue<string> gpsTrame; // Used to stock ALL trames GPS Message
        private Queue<ObjectGP> gpsTrameParsed;
        private string actualStatus = StatusEnum.ConnectionKO.ToString();

        string recieved_data; // Used to stock one trame of the GPS Message
        private string link; // Used to stock the path for Ports.xml
        ObjectsPorts objports = new ObjectsPorts();//Used to be the list of ObjectPort from Ports.xml
        SerialPort sp = new SerialPort(); // Used to be THE SerialPort of NavRTK
        private string[] portName;//Used to stock all ports names availables
        private string onOffButton = "Read";// Used to stock the content of the switch button
        private string[] enumBauds = { "115200", "4800", "9600" };// Used to stock all bauds availables
        private string[] enumDatabits = { "8", "7", "6", "5" };// Used to stock all bits availables
        private string[] enumStopbit = { "One", "OntPointFive", "Two", "None" };// Used to stock all the stopbits availables
        private string[] enumParity = { "None", "Even", "Mark", "Odd", "Space" };// Used to stock all parity availables
        private string[] enumHandshake = { "None", "XOnXOff", "RequestToSend", "RequestToSendXOnXOff" };// Used to stock all handshake available
        private string selectedName; // Used to stock the name selected in the popup window
        private string selectedBauds;// Used to stock the baud selected in the popup window
        private string selectedDatabits;// Used to stock the databits selected in the popup window
        private string selectedStopbits;// Used to stock the stopbit selected in the popup window 
        private string selectedParity;// Used to stock the parity selected in the popup window
        private string selectedHandshake;// Used to stock the handshake selected in the popup window
        private ObjectPort selectedObjectPort;// Used to be the ObjectPort selected for recieved data
        private bool isOpen; // Used to determind if the popup "new port" is hidden or not

        /*** GPS FIELDS ***
         Used to fill in the elements of the view
         */
        //Both GGA & RMC
        private string time;
        private string latitude;
        private string longitude;

        //GGA
        private string position;
        private string quality;
        private string nSat;
        private string dilution;
        private string altitude;
        private string geoidal;
        private string GGAlastDGPS;

        //RMC
        private string validity;
        private string speed;
        private double speedBar;
        private string cap;
        private string magnetic;
        private string modepos;
        /*** GPS FIELDS END ***/
        #endregion FIELDS

        #region PROPERTIES
        /// <summary>
        /// Determine if the PopUp is open or not
        /// </summary>
        public bool IsOpen
        {
            get { return isOpen; }
            set
            {
                if (isOpen == value) return;
                isOpen = value;
                OnPropertyChanged("IsOpen");
            }
        }
        public string[] EnumBauds
        {
            get { return enumBauds; }
            set
            {
                if (enumBauds == value) return;
                enumBauds = value;
                OnPropertyChanged("EnumBauds");
            }
        }
        public string[] EnumDatabits
        {
            get { return enumDatabits; }
            set
            {
                if (enumDatabits == value) return;
                enumDatabits = value;
                OnPropertyChanged("EnumDatabits");
            }
        }
        public string[] EnumHandshake
        {
            get { return enumHandshake; }
            set
            {
                if (enumHandshake == value) return;
                enumHandshake = value;
                OnPropertyChanged("EnumHandshake");
            }
        }
        public string[] EnumParity
        {
            get { return enumParity; }
            set
            {
                if (enumParity == value) return;
                enumParity = value;
                OnPropertyChanged("EnumParity");
            }
        }
        public string[] EnumStopbit
        {
            get { return enumStopbit; }
            set
            {
                if (enumStopbit == value) return;
                enumStopbit = value;
                OnPropertyChanged("EnumStopbit");
            }
        }
        public string OnOffButton
        {
            get
            {
                ActualStatus = StatusEnum.ConnectionLoading.ToString();
                OnPropertyChanged("ActualStatus");
                return onOffButton; }
            set
            {
                
                onOffButton = value;
                OnPropertyChanged("OnOffButton");
            }
        }
        public string[] PortName
        {
            get
            {
                portName = SerialPort.GetPortNames();
                return portName;
            }
            set
            {
                portName = value;
                OnPropertyChanged("PortName");
            }
        }
        public string SelectedName
        {
            get
            {
                return selectedName;
            }
            set
            {
                selectedName = value;
                OnPropertyChanged("SelectedName");
            }
        }
        public string SelectedBauds
        {
            get
            {
                return selectedBauds;
            }
            set
            {
                selectedBauds = value;
                OnPropertyChanged("SelectedBauds");
            }
        }
        public string SelectedDatabits
        {
            get
            {
                return selectedDatabits;
            }
            set
            {
                selectedDatabits = value;
                OnPropertyChanged("SelectedDatabits");
            }
        }
        public string SelectedStopbits
        {
            get
            {
                return selectedStopbits;
            }
            set
            {
                selectedStopbits = value;
                OnPropertyChanged("SelectedStopbits");
            }
        }
        public string SelectedParity
        {
            get
            {
                return selectedParity;
            }
            set
            {
                selectedParity = value;
                OnPropertyChanged("SelectedParity");
            }
        }
        public string SelectedHandshake
        {
            get
            {
                return selectedHandshake;
            }
            set
            {
                selectedHandshake = value;
                OnPropertyChanged("SelectedHandshake");
            }
        }
        public ObjectPort SelectedPort
        {
            get { return selectedObjectPort; }
            set
            {
                if (selectedObjectPort != value)
                {
                    selectedObjectPort = value;
                    OnPropertyChanged("SelectedPort");
                }
            }
        }
        public string GpsTrame
        {
            get
            {
                string alltrames = "";
                foreach (string o in gpsTrame)
                    alltrames += o.ToString();
                return alltrames;
            }
            set
            {
                gpsTrame.Enqueue(value);
                OnPropertyChanged("GpsTrame");
            }
        }
        public ObjectsPorts ObjPorts
        {
            get
            {
                return objports;
            }
            set
            {
                objports = value;
                OnPropertyChanged("ObjPorts");
            }
        }
        public string ActualStatus
        {
            get
            { return actualStatus; }
            set
            {
                actualStatus = value;
                OnPropertyChanged("ActualStatus");
            }
        }
        /// <summary>
        /// Both
        /// </summary>
        #region Get Set Both
        public string Time
        {
            get
            { return time; }
            set
            {
                time = value;
                OnPropertyChanged("Time");
            }
        }
        public string Latitude
        {
            get
            { return latitude; }
            set
            {
                latitude = value;
                OnPropertyChanged("Latitude");
            }
        }
        public string Longitude
        {
            get
            { return longitude; }
            set
            {
                longitude = value;
                OnPropertyChanged("Longitude");
            }
        }
        #endregion

        /// <summary>
        /// GGA
        /// </summary>
        #region Get Set GGA
        public string Position
        {
            get
            { return position; }
            set
            {
                position = value;
                OnPropertyChanged("Position");
            }
        }
        public string Quality
        {
            get
            { return quality; }
            set
            {
                quality = value;
                OnPropertyChanged("Quality");
            }
        }
        public string NSat
        {
            get
            { return nSat; }
            set
            {
                nSat = value;
                OnPropertyChanged("NSat");
            }
        }
        public string Dilution
        {
            get
            { return dilution; }
            set
            {
                dilution = value;
                OnPropertyChanged("Dilution");
            }
        }
        public string Altitude
        {
            get
            { return altitude; }
            set
            {
                altitude = value;
                OnPropertyChanged("Altitude");
            }
        }
        public string Geoidal
        {
            get
            { return geoidal; }
            set
            {
                geoidal = value;
                OnPropertyChanged("Geoidal");
            }
        }
        public string GGALastDGPS
        {
            get
            { return GGAlastDGPS; }
            set
            {
                GGAlastDGPS = value;
                OnPropertyChanged("GGALastDGPS");
            }
        }
        public string Cap
        {
            get
            { return cap; }
            set
            {
                cap = value;
                OnPropertyChanged("Cap");
            }
        }
        #endregion GetSet

        /// <summary>
        /// RMC
        /// </summary>
        #region Get Set RMC
        public string Validity
        {
            get
            { return validity; }
            set
            {
                validity = value;
                OnPropertyChanged("Validity");
            }
        }
        public string Speed
        {
            get
            { return speed; }
            set
            {
                speed = value;
                OnPropertyChanged("Speed");
            }
        }
        public double SpeedBar
        {
            get
            { return speedBar; }
            set
            {
                speedBar = value;
                OnPropertyChanged("SpeedBar");
            }
        }
        public string Magnetic
        {
            get
            { return magnetic; }
            set
            {
                magnetic = value;
                OnPropertyChanged("Magnetic");
            }
        }
        public string ModePos
        {
            get
            { return modepos; }
            set
            {
                modepos = value;
                OnPropertyChanged("ModePos");
            }
        }
        #endregion
        #endregion PROPERTIES

        #region COMMANDS
        /// <summary>
        /// Open the popup "new port settings"
        /// </summary>
        private RelayCommand openPopUp;
        public ICommand OpenPopUp
        {
            get
            {
                if (openPopUp == null)
                {
                    openPopUp = new RelayCommand(ExecuteOpenPopUp, CanOpenPopUp);
                }
                return openPopUp;
            }
        }

        /// <summary>
        /// Close the popup "new port settings"
        /// </summary>
        private RelayCommand closePopUp;
        public ICommand ClosePopUp
        {
            get
            {
                if (closePopUp == null)
                {
                    closePopUp = new RelayCommand(ExecuteClosePopUp, CanClosePopUp);
                }
                return closePopUp;
            }
        }

        /// <summary>
        /// Save the new port to Ports.xml
        /// </summary>
        private RelayCommand validationNewPort;
        public ICommand ValidationNewPort
        {
            get
            {
                if (validationNewPort == null)
                {
                    validationNewPort = new RelayCommand(ExecuteValidationNewPort, CanValidationNewPort);
                }
                return validationNewPort;
            }
        }

        /// <summary>
        /// Stop receiving data
        /// </summary>
        private RelayCommand stop;
        public ICommand Stop
        {
            get
            {
                if (stop == null)
                {
                    stop = new RelayCommand(ExecuteStop, CanStop);
                }
                return stop;
            }
        }

        /// <summary>
        ///  Delete an ObjectPort from the list and Ports.xml
        /// </summary>
        private RelayCommand listBoxDeleteItem;
        public ICommand ListBoxDeleteItem
        {
            get
            {
                if (listBoxDeleteItem == null)
                {
                    listBoxDeleteItem = new RelayCommand(ExecuteListBoxDeleteItem, CanListBoxDeleteItem);
                }
                return listBoxDeleteItem;
            }
        }

        /// <summary>
        ///  Delete an ObjectPort from the list and Ports.xml
        /// </summary>
        private RelayCommand listBoxDefaultItem;
        public ICommand ListBoxDefaultItem
        {
            get
            {
                if (listBoxDefaultItem == null)
                {
                    listBoxDefaultItem = new RelayCommand(ExecuteListBoxDefaultItem, CanListBoxDefaultItem);
                }
                return listBoxDefaultItem;
            }
        }

        private RelayCommand switchToDataParsedView;
        public ICommand SwitchToDataParsedView
        {
            get
            {
                if (switchToDataParsedView == null)
                {
                    switchToDataParsedView = new RelayCommand(ExecuteSwitchToDataParsedView, CanSwitchToDataParsedView);
                }
                return switchToDataParsedView;
            }
        }
        
        private RelayCommand switchToSettingsView;
        public ICommand SwitchToSettingsView
        {
            get
            {
                if (switchToSettingsView == null)
                {
                    switchToSettingsView = new RelayCommand(ExecuteSwitchToSettingsView, CanSwitchToSettingsView);
                }
                return switchToSettingsView;
            }
        }
        #endregion

        #region CONSTRUCTOR
        [ImportingConstructor]
        public HomeViewModel()
        {
            link = "../../Ports.xml";
            portName = SerialPort.GetPortNames();
            gpsTrame = new Queue<string>();
            gpsTrameParsed = new Queue<ObjectGP>();
            XMLtoSerialPort();
        }
        #endregion CONSTRUCTOR

        #region COMMANDS IMPLEMENTATION
        private bool CanListBoxDeleteItem() { return true; }
        private void ExecuteListBoxDeleteItem()
        {
            int id = -1;

            if (selectedObjectPort != null)
                id = selectedObjectPort.Id;
            foreach (ObjectPort o in objports)
            {
                if (o.Id == id)
                {
                    objports.Remove(o);
                    break;
                }
            }
            objports.Enregistrer(link);
            XMLtoSerialPort();
            OnPropertyChanged("ObjPorts");
            OnPropertyChanged("ListBoxDeleteItem");
        }
        private bool CanListBoxDefaultItem() { return true; }
        private void ExecuteListBoxDefaultItem()
        {
            try
            {
                int id = -1;

                if (selectedObjectPort != null)
                    id = selectedObjectPort.Id;

                if (File.Exists(link))
                {
                    objports = ObjectsPorts.Charger(link);
                }
                objports.DefaultSwap(id);

                objports.Sort(delegate(ObjectPort op1, ObjectPort op2)
                {
                    return op1.Id.CompareTo(op2.Id);
                });

                objports.Enregistrer(link);
                XMLtoSerialPort();
                OnPropertyChanged("ObjPorts");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

        }
        private bool CanValidationNewPort()
        { return true; }
        private void ExecuteValidationNewPort()
        {
            isOpen = false;
            OnPropertyChanged("IsOpen");

            if (File.Exists(link))
            {
                objports = ObjectsPorts.Charger(link);
            }
            else
            {
                File.WriteAllText(link, "<?xml version=\"1.0\" encoding=\"utf-8\"?><ArrayOfObjectPort/>");
                objports = new ObjectsPorts();
            }

            ObjectPort obj = new ObjectPort()
            {
                Id = objports.MaxId() + 1,
                Name = selectedName,
                Baudrate = selectedBauds,
                Databits = selectedDatabits,
                Stopbit = selectedStopbits,
                Parity = selectedParity,
                Handshake = selectedHandshake,
            };

            objports.Add(obj);
            objports.Enregistrer(link);

            OnPropertyChanged("ObjPorts");
        }
        private void ExecuteStop()
        {
            if (onOffButton == "Break")
            {
                sp.Close();
                onOffButton = "Read";
            }
            else
            {
                if (selectedObjectPort == null)
                {
                    initSerialPort(objports);
                }
                else
                {
                    sp.PortName = selectedObjectPort.Name;
                    sp.BaudRate = int.Parse(selectedObjectPort.Baudrate);
                }

                if (sp != null)
                    if (!sp.IsOpen)
                        sp.Open();

                //Sets button State and Creates function call on data recieved
                sp.DataReceived += new SerialDataReceivedEventHandler(Recieve);

                onOffButton = "Break";
                if (!sp.IsOpen)
                    sp.Open();
            }
            OnPropertyChanged("OnOffButton");
        }
        private bool CanStop()
        {
            return true;
        }
        private bool CanOpenPopUp()
        { return true; }
        private void ExecuteOpenPopUp()
        {
            isOpen = true;
            OnPropertyChanged("IsOpen");
        }
        private bool CanClosePopUp()
        { return true; }
        private void ExecuteClosePopUp()
        {
            isOpen = false;
            OnPropertyChanged("IsOpen");
        }

        private bool CanSwitchToDataParsedView()
        {
            return true;
        }
        private void ExecuteSwitchToDataParsedView()
        {
            System.Console.WriteLine("DataClicked");
        }
        private bool CanSwitchToSettingsView()
        {
            return true;
        }
        private void ExecuteSwitchToSettingsView()
        {           
            System.Console.WriteLine("SettingsClicked");
        }
        #endregion

        #region RECIEVING

        public delegate void UpdateUiTextDelegate1(MessageGPGGA objGGA);
        public delegate void UpdateUiTextDelegate2(MessageGPRMC objRMC);
        private void Recieve(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                if (sp.IsOpen == true)
                    recieved_data = sp.ReadLine();
            }
            catch (Exception exp)
            {
                Console.WriteLine(" recieve " + exp.Message);
            }

            if (recieved_data != null && !recieved_data.Contains("$GP"))
            {
                recieved_data = "Recieved data can't be used, please change the serialport by default\n";
                ActualStatus = StatusEnum.ConnectionKO.ToString();
                OnPropertyChanged("ActualStatus");

            }
            else 
            {
                GPSParsor.splitMessage(recieved_data, gpsTrameParsed);
                ActualStatus = StatusEnum.ConnectionOK.ToString();
                OnPropertyChanged("ActualStatus");
            }

            if (gpsTrameParsed != null) 
            {
                if (gpsTrameParsed.Last().GetType() == typeof(MessageGPGGA))
                {
                    Application.Current.Dispatcher.Invoke(DispatcherPriority.Send, new UpdateUiTextDelegate1(WriteDataGGA), gpsTrameParsed.Last());
                }
                else if (gpsTrameParsed.Last().GetType() == typeof(MessageGPRMC))
                {
                    Application.Current.Dispatcher.Invoke(DispatcherPriority.Send, new UpdateUiTextDelegate2(WriteDataRMC), gpsTrameParsed.Last());
                }
            }

            if (gpsTrame != null)
            {
                if (gpsTrame.Count > 20)
                    gpsTrame.Dequeue();

                if (recieved_data != null)
                    gpsTrame.Enqueue(recieved_data);

                OnPropertyChanged("GpsTrame");
            }
        }
        #endregion RECIEVING

        #region METHODS
        public void XMLtoSerialPort()
        {
            if (File.Exists(link))
            {
                objports = ObjectsPorts.Charger(link);
            }
            else
            {
                objports = new ObjectsPorts();
            }
            objports.Enregistrer(link);
            OnPropertyChanged("ObjPorts");
        }

        /// <summary>
        /// Used to initialize the object port 
        /// </summary>
        /// <param name="objports">list of ObjectPort</param>
        public void initSerialPort(ObjectsPorts objports)
        {
            foreach (ObjectPort o in objports)
            {
                if (o.Id == 0)
                {
                    sp = new SerialPort();
                    sp.PortName = o.Name;
                    sp.BaudRate = int.Parse(o.Baudrate);
                    sp.DataBits = int.Parse(o.Databits);
                    switch (o.Stopbit)
                    {
                        case "One":
                            sp.StopBits = StopBits.One;
                            break;
                        case "Two":
                            sp.StopBits = StopBits.Two;
                            break;
                        case "OnePointFive":
                            sp.StopBits = StopBits.OnePointFive;
                            break;
                        default: sp.StopBits = StopBits.None;
                            break;
                    }
                    switch (o.Parity)
                    {
                        case "Even":
                            sp.Parity = Parity.Even;
                            break;
                        case "Mark":
                            sp.Parity = Parity.Mark;
                            break;
                        case "Odd":
                            sp.Parity = Parity.Odd;
                            break;
                        case "Space":
                            sp.Parity = Parity.Space;
                            break;
                        default: sp.Parity = Parity.None;
                            break;
                    }
                    switch (o.Handshake)
                    {
                        case "One":
                            sp.Handshake = Handshake.XOnXOff;
                            break;
                        case "Two":
                            sp.Handshake = Handshake.RequestToSend;
                            break;
                        case "OnePointFive":
                            sp.Handshake = Handshake.RequestToSendXOnXOff;
                            break;
                        default: sp.Handshake = Handshake.None;
                            break;
                    }
                }
            }
        }

        /// <summary>
        /// Used to load GGA fields
        /// </summary>
        /// <param name="objGGA"></param>
        public void WriteDataGGA(MessageGPGGA objGGA)
        {

            time = objGGA.timeUTC.ToString("d MMMM yyyy hh:mm:ss");
            OnPropertyChanged("Time");

            latitude = objGGA.latitude.ToString("F6");
            OnPropertyChanged("Latitude");

            longitude = objGGA.longitude.ToString("F6");
            OnPropertyChanged("Longitude");

            position = objGGA.gpsQuality.ToString();
            OnPropertyChanged("Position");

            nSat = objGGA.nSat.ToString();


            // nSat = (DateTime.Now.Second % 10).ToString();  // test color
            OnPropertyChanged("NSat");
            dilution = objGGA.dilution.ToString("F2");
            OnPropertyChanged("Dilution");

            altitude = objGGA.altitude.ToString("F2") + objGGA.altUnit.ToString();
            OnPropertyChanged("Altitude");

            geoidal = objGGA.geoidal.ToString("F2") + objGGA.geoUnit.ToString();
            OnPropertyChanged("Geoidal");

            GGAlastDGPS = objGGA.dGPSTime.ToString("d MMM yyyy");
            OnPropertyChanged("GGALastDGPS");

        }
        /// <summary>
        /// Used to load RMC fields
        /// </summary>
        /// <param name="objRMC"></param>
        public void WriteDataRMC(MessageGPRMC objRMC)
        {
            validity = objRMC.status.ToString();
            OnPropertyChanged("Validity");

            /*PREVISIONNEL*/
            speed = objRMC.speed.ToString();
            OnPropertyChanged("Speed");

            speedBar = double.Parse(speed) * 1000;
            OnPropertyChanged("SpeedBar");

            cap = objRMC.cap.ToString();
            OnPropertyChanged("Cap");

            magnetic = objRMC.magnetic.ToString();
            OnPropertyChanged("Magnetic");

            modepos = objRMC.integrity;
            OnPropertyChanged("ModePos");

        }
        #endregion METHODS

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }
        #endregion INotifyPropertyChanged Members
    }
}
