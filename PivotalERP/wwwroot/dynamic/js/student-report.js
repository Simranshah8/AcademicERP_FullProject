var columnDefs = [
    {headerName:"All",field:"All",headerCheckboxSelection: true,checkboxSelection: true, width:"150px"},
	{headerName:"Id", field: "Id",filter:true, width:"80px",filter: "agNumberColumnFilter"},
	{headerName:"Name", field: "Name",width:"200px",filter:'agTextColumnFilter' },
    {headerName:"Roll No." ,field: "Roll No.",width:"100px",filter: "agNumberColumnFilter"},
    {headerName:"Gender",field: "Gender",width:"100px",filter:true},
    {headerName:"Class/Sec.",  field: "Class/Sec",width:"150px",filter:true},
    {headerName:"DOB",  field: "DOB",width:"100px",filter:true},
    {headerName:"Father's Name",  field: "Father's Name",width:"200px",filter:true},
    {headerName:"Contact No.",  field: "Contact No",width:"150px",filter:true},
    {headerName:"Address",  field: "Address",width:"150px",filter:true},
    
  ];

//   var autoGroupColumnDef = {
// 	  headerName: "Model",
// 	  field: "model",
// 	  cellRenderer:'agGroupCellRenderer',
// 	  cellRendererParams: {
// 		  checkbox: true
// 	  }
//   }

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
	Id:111,
	Name:'Ram Prasad',
    RollNo :'1233',
});
dataColl.push({
	Id:111,
	Name:'Ram Prasad',
    RollNo :'1233',
});

gridOptions.api.setRowData(dataColl);


// For New Admission Tab

var columnDefs = [
    {field:"All",width:"150px",headerCheckboxSelection: true,checkboxSelection: true},
	{ field: "Id",sortable:true,width:"80px",filter:true},
	{ field: "Name",sortable:true,width:"200px",filter:true},
    { field: "Roll No.",sortable:true,width:"100px",filter:true},
    { field: "Regd.No.",sortable:true,width:"100px",filter:true},
    { field: "Admit Date",sortable:true,width:"150px",filter:true},
    { field: "Class/Sec",sortable:true,width:"200px",filter:true},
    { field: "DOB",sortable:true,width:"100px",filter:true},
    { field: "Father's Name",sortable:true,width:"200px",filter:true},
    { field: "Contact No",sortable:true,width:"200px",filter:true},
    { field: "Address",sortable:true,width:"150px",filter:true},
    
  ];

  // let the grid know which columns and what data to use
  var gridOptionsnew = {
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
var eGridDivnew = document.querySelector('#myGridnew');


// create the grid passing in the div to use together with the columns & data we want to use
new agGrid.Grid(eGridDivnew, gridOptionsnew);
var dataColl1=[];
dataColl1.push({
	Id:111,
	Name:'Hari Kumar',
    RollNo :'1233',
});
dataColl1.push({
	Id:111,
	Name:'Shyam Paudel',
    RollNo :'1233',
});

gridOptionsnew.api.setRowData(dataColl1);


// For Left Student list


var columnDefs = [
    {headerName:"All",field:"All",width:"150px",headerCheckboxSelection: true,checkboxSelection: true},
	{headerName:"Id", field: "Id",width:"80px",},
	{headerName:"Name", field: "Name",width:"200px",},
    {headerName:"Roll No.", field: "Roll No.",width:"100px",},
    {headerName:"Gender", field: "Gender",width:"150px",},
    {headerName:"Class/Sec.", field: "Class/Sec",width:"200px",},
    {headerName:"DOB", field: "DOB",width:"150px",},
    {headerName:"Father's Name", field: "Father's Name",width:"200px",},
    {headerName:"Contact No.", field: "Contact No",width:"200px",},
    {headerName:"Address", field: "Address",width:"150px",},
    
  ];

  // let the grid know which columns and what data to use
  var gridOptionsleft = {
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
var eGridDivleft = document.querySelector('#myGridleft');


// create the grid passing in the div to use together with the columns & data we want to use
new agGrid.Grid(eGridDivleft, gridOptionsleft);
var dataColl3=[];
dataColl3.push({
	Id:111,
	Name:'Suresh',
    RollNo :'1233',
});
dataColl3.push({
	Id:111,
	Name:'Raju',
    RollNo :'1233',
});

gridOptionsleft.api.setRowData(dataColl3);



// For Medium Wise list


var columnDefs = [
    {headerName:"All",field:"All",headerCheckboxSelection: true,checkboxSelection: true},
	{headerName:"Id", field: "Id",sortable:true,filter:true},
	{headerName:"Name", field: "Name",sortable:true,filter:true},
    {headerName:"Roll No.", field: "Roll No.",sortable:true,filter:true},
    {headerName:"Gender", field: "Gender",sortable:true,filter:true},
    {headerName:"Class/Sec.", field: "Class/Sec",sortable:true,filter:true},
    {headerName:"DOB", field: "DOB",sortable:true,filter:true},
    {headerName:"Father's Name", field: "Father's Name",sortable:true,filter:true},
    {headerName:"Contact No.", field: "Contact No",sortable:true,filter:true},
    {headerName:"Address", field: "Address",sortable:true,filter:true},
    
  ];

  // let the grid know which columns and what data to use
  var gridOptionsmedium = {
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
var eGridDivmedium = document.querySelector('#myGridmedium');


// create the grid passing in the div to use together with the columns & data we want to use
new agGrid.Grid(eGridDivmedium, gridOptionsmedium);
var dataColl4=[];
dataColl4.push({
	Id:111,
	Name:'Pnkaj',
    RollNo :'1233',
});
dataColl4.push({
	Id:111,
	Name:'Rohit',
    RollNo :'1233',
});

gridOptionsmedium.api.setRowData(dataColl4);

// For Shift Wise list


var columnDefs = [
    {headerName:"All", field:"All",headerCheckboxSelection: true,checkboxSelection: true,width:150},
	{headerName:"Id", field: "Id",sortable:true,filter:true,width:100},
	{headerName:"Name", field: "Name",sortable:true,filter:true ,width:200},
    {headerName:"Roll No.", field: "Roll No.",sortable:true,filter:true ,width:100},
    {headerName:"Gender", field: "Gender",sortable:true,filter:true ,width:100},
    {headerName:"Class/Sec.", field: "Class/Sec",sortable:true,filter:true ,width:150},
    {headerName:"DOB", field: "DOB",sortable:true,filter:true ,width:100},
    {headerName:"Father's Name", field: "Father's Name",sortable:true,filter:true ,width:200},
    {headerName:"Contact No.", field: "Contact No",sortable:true,filter:true ,width:150},
    {headerName:"Address", field: "Address",sortable:true,filter:true ,width:150},
    
  ];

  // let the grid know which columns and what data to use
  var gridOptionsshift = {
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
var eGridDivshift = document.querySelector('#myGridshift');


// create the grid passing in the div to use together with the columns & data we want to use
new agGrid.Grid(eGridDivshift, gridOptionsshift);
var dataColl5=[];
dataColl5.push({
	Id:111,
	Name:'Gopal',
    RollNo :'1233',
});
dataColl5.push({
	Id:111,
	Name:'Rohit',
    RollNo :'1233',
});

gridOptionsshift.api.setRowData(dataColl5);

