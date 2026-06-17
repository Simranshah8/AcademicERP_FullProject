// specify the columns

var columnDefs = [
	{headerName:"Id", field: "Id", width:"80px",filter: "agNumberColumnFilter" },
	{headerName:"Code", field: "Code" ,width:"100px",filter: "agNumberColumnFilter" },
    {headerName:"Name", field: "Name",width:"150px",filter:'agTextColumnFilter' },
    {headerName:"Gender", field: "Gender",width:"100px", },
    {headerName:"Designation", field: "Designation" ,width:"150px",filter:'agTextColumnFilter'},
    {headerName:"Email", field: "Email",width:"200px",filter:'agTextColumnFilter' },
    {headerName:"Date of Birth", field: "Date of Birth",width:"150px" ,},
    {headerName:"Pan Id", field: "Pan Id",width:"100px",filter: "agNumberColumnFilter"},
    {headerName:"Contact", field: "Contact",width:"150px",filter: "agNumberColumnFilter" },
    {headerName:"Department", field: "Department" ,width:"150px",filter:'agTextColumnFilter'},
    {headerName:"Category", field: "Category" ,width:"150px",filter:'agTextColumnFilter'},
    {headerName:"Level", field: "Level" ,width:"150px",},
    {headerName:"Join Date", field: "Join Date",width:"100px", },
    {headerName:"Caste", field: "Caste" ,width:"100px",filter:'agTextColumnFilter'},
  ];


  // let the grid know which columns and what data to use
  var gridOptions = {
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

// lookup the container we want the Grid to use
var eGridDiv = document.querySelector('#myGrid');

// create the grid passing in the div to use together with the columns & data we want to use
new agGrid.Grid(eGridDiv, gridOptions);
var dataColl=[];
dataColl.push({
	Id:'1',
	Name:'Ram'
});
gridOptions.api.setRowData(dataColl);



var columnDefs = [
    {headerName:"S.No." ,field: "S.No.", width:100,filter: "agNumberColumnFilter" },
    {headerName:"Code" , field: "Code", width:100,filter: "agNumberColumnFilter" },
    {headerName:"Name" , field: "Name", width:250,filter:'agTextColumnFilter'},
    {headerName:"Contact No." , field: "Contact No.", width:250,filter: "agNumberColumnFilter" },
    {headerName:"Left Date" , field: "Left Date", width:200,},
    {headerName:"Remarks" , field: "Remarks", width:350,filter:'agTextColumnFilter'},
]

var gridOptions1 = {
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
  var eGridDiv1 = document.querySelector('#employee-left-table');
  new agGrid.Grid(eGridDiv1, gridOptions1);
var dataColl1=[];
dataColl1.push({
	Code:'112',
	Name:'Ram'
});
gridOptions1.api.setRowData(dataColl1);