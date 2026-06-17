// specify the columns

var columnDefs = [
	{headerName: "Photo", field: "Photo", width:150,},
	{headerName: "Id", field: "Id" ,width:100,filter: "agNumberColumnFilter"},
    {headerName: "Regd. No.",  field: "Regd. No.",width:150,filter: "agNumberColumnFilter" },
    {headerName: "Class/Sec",  field: "Class/Sec",width:250,filter:'agTextColumnFilter' },
    {headerName: "Roll No.",  field: "Roll No." ,width:150,filter: "agNumberColumnFilter"},
    {headerName: "Name",  field: "Name",width:250,filter:'agTextColumnFilter' },
    {headerName: "Gender",  field: "Gender",width:200 ,},
    {headerName: "Date Of Birth(AD)",  field: "Date of Birth(AD)",width:250,},
    {headerName: "Date Of Birth(BS)",  field: "Date of Birth(BS)",width:250 ,},
    {headerName: "Birthday",  field: "Birthday" ,width:150,},
    {headerName: "Parent's Contact No.",  field: "Parent's Contact No." ,width:250,filter: "agNumberColumnFilter"},
    {headerName:"Address", field: "Address" ,width:150,filter:'agTextColumnFilter'},
    {headerName:"Age", field: "Age",width:100, filter: "agNumberColumnFilter"},
   
  ];


  // let the grid know which columns and what data to use
  var gridOptions = {
    columnDefs: columnDefs,
    rowHeight: 31,
    headerHeight: 31,
    defaultColDef: {
     
      cellStyle: { 'line-height':'31px'},
      minWidth:100,
  sortable: true, 
  filter: true,
  resizable: true,
  
	rowSelection: 'multiple'
  },
	
  };

// lookup the container we want the Grid to use
var eGridDiv = document.querySelector('#birthday-table');

// create the grid passing in the div to use together with the columns & data we want to use
new agGrid.Grid(eGridDiv, gridOptions);
var dataColl=[];
dataColl.push({
	Id:'1',
	Name:'Ram'
});
gridOptions.api.setRowData(dataColl);


// Employee Bithday
var columnDefs = [
    {headerName:"Photo", field: "Photo", width:150},
	{headerName:"Id", field: "Id" ,width:100,filter: "agNumberColumnFilter"},
    {headerName:"Code", field: "Code",width:150,filter: "agNumberColumnFilter" },
    {headerName:"Designation", field: "Designation",width:200 },
    {headerName:"Name", field: "Name",width:250 ,filter:'agTextColumnFilter'},
    {headerName:"Gender", field: "Gender",width:200 ,filter:'agTextColumnFilter'},
    {headerName:"Date Of Birth(AD)", field: "Date of Birth(AD)",width:250,},
    {headerName:"Date of Birth(BS)", field: "Date of Birth(BS)",width:250 ,},
    {headerName:"Birthday", field: "Birthday" ,width:150},
    {headerName:"Parent's Contact No.", field: "Parent's Contact No." ,width:250,filter: "agNumberColumnFilter"},
    {headerName:"Address", field: "Address" ,width:150,filter:'agTextColumnFilter'},
    {headerName:"Age",  field: "Age",width:100,filter: "agNumberColumnFilter" },
]

var gridOptions1 = {
  columnDefs: columnDefs,
  rowHeight: 31,
  headerHeight: 31,
  defaultColDef: {
    
sortable: true, 
filter: true,
resizable: true,
cellStyle: { 'line-height':'31px'},

rowSelection: 'multiple'
},
  };
  var eGridDiv1 = document.querySelector('#employee-birthday');
  new agGrid.Grid(eGridDiv1, gridOptions1);
var dataColl1=[];
dataColl1.push({
	Code:'112',
	Name:'Ram'
});
gridOptions1.api.setRowData(dataColl1);


// Fee Module ->Additional->discount-type-wise-student-list

var columnDefs = [
  
{headerName:"Id", field: "Id" ,width:100,filter: "agNumberColumnFilter"},
{headerName:"Regd No.", field: "Regd No.", width:100,filter: "agNumberColumnFilter"},
  {headerName:"Roll No.", field: "Roll No.",width:100,filter: "agNumberColumnFilter" },
  {headerName:"Name", field: "Name",width:250 ,filter:'agTextColumnFilter'},
  {headerName:"Class", field: "Class",width:200,filter:'agTextColumnFilter' },
  {headerName:"Discount Type", field: "Discount Type",width:200 },
  {headerName:"Father Name", field: "Father Name",width:200,filter:'agTextColumnFilter' },
  {headerName:"Contact No.", field: "Contact No." ,width:250,filter: "agNumberColumnFilter"},
  {headerName:"Address", field: "Address" ,width:150},
  {headerName:"Transport Point", field: "Transport Point",width:250 },
  {headerName:"Boarders Name", field: "Boarders Name" ,width:150,filter:'agTextColumnFilter'},
  {headerName:"House Name", field: "House Name" ,width:150,filter:'agTextColumnFilter'},
  {headerName:"Remarks", field: "Remarks" ,width:150,filter:'agTextColumnFilter'},
  {headerName:"Is Left",  field: "Is Left",width:100 }
]

var gridOptionsAdditional = {
columnDefs: columnDefs,
rowHeight: 31,
headerHeight: 31,

defaultColDef: {
  resizable: true,
sortable: true, 
filter: true,
resizable: true,
cellStyle: { 'line-height':'31px'},

rowSelection: 'multiple'
},
};
var eGridDivAdditional = document.querySelector('#discount-type-wise-student-list');
new agGrid.Grid(eGridDivAdditional, gridOptionsAdditional);
var dataCollAdditional=[];
dataCollAdditional.push({
  
Name:'Ram',
Class:'A'
});
gridOptionsAdditional.api.setRowData(dataCollAdditional);
