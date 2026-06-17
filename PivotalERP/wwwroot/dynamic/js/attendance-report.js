var columnDefs = [
    
	{headerName:"Id", field: "Id",filter:true, width:"80px",filter: "agNumberColumnFilter"},
	{headerName:"Regd. No." ,field: "Roll No.",width:"130px",filter: "agNumberColumnFilter"},
    {headerName:"Roll No." ,field: "Roll No.",width:"130px",filter: "agNumberColumnFilter"},
    {headerName:"Name", field: "Name",width:"200px",filter:'agTextColumnFilter' },
    {headerName:"Class",field: "Class",width:"100px",filter:true},
    {headerName:"Attendance",  field: "Attendance",width:"150px",filter:true},
    {headerName:"In Time",  field: "In Time",width:"80px",filter:true},
    {headerName:"Out Time",  field: "Out Time",width:"80px",filter:true},
    {headerName:"Late Min.",  field: "In Time",width:"80px",filter:true},
    {headerName:"Enroll No.",  field: "Enroll No.",width:"80px",filter:true},
    {headerName:"Father's Name",  field: "Father's Name",width:"200px",filter:true},
    {headerName:"Father Contact No.",  field: "Contact No",width:"150px",filter:true},
  
    
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
   
	{headerName:"Date", field: "Date",width:"100px",},
	{headerName:"Day" ,field: "Day",width:"100px",filter: "agTextColumnFilter"},
    {headerName:"Class",field: "Class",width:"100px",filter: "agTextColumnFilter"},
    {headerName:"Section" ,field: "Section",width:"100px",filter: "agTextColumnFilter"},
    {headerName:"Subject" ,field: "Subject",width:"100px",filter: "agTextColumnFilter"},
    {headerName:"Name", field: "Name",width:"200px",filter:'agTextColumnFilter' },
   
    {headerName:"Roll No.",  field: "Roll No.",width:"130px",},
    {headerName:"Class Start Time",  field: "Class Start Time",width:"180px",},
    {headerName:"Class End Time",  field: "Class End Time",width:"180px",},
    {headerName:"Join Time",  field: "Join Time",width:"80px",},
    {headerName:"Leave Time",  field: "Leave Time",width:"80px",},
    {headerName:"Teacher",  field: "Teacher",width:"100px",},
    {headerName:"Father's Name",  field: "Father's Name",width:"200px",},
    {headerName:"Contact No.",  field: "Contact No",width:"150px",},
    {headerName:"Joint With",  field: "Joint With",width:"100px",},
    {headerName:"Attendance",  field: "Attendance",width:"100px",},
    
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
    {headerName:"Id", field: "Id",filter:true, width:"80px",filter: "agNumberColumnFilter"},
	{headerName:"Regd. No." ,field: "Roll No.",width:"130px",filter: "agNumberColumnFilter"},
    {headerName:"Roll No." ,field: "Roll No.",width:"130px",filter: "agNumberColumnFilter"},
    {headerName:"Name", field: "Name",width:"200px",filter:'agTextColumnFilter' },
    {headerName:"Class",field: "Class",width:"100px",filter:true},
   
    {headerName:"In Time",  field: "In Time",width:"80px",filter:true},
    {headerName:"Out Time",  field: "Out Time",width:"80px",filter:true},
    {headerName:"Attendance",  field: "Attendance",width:"150px",filter:true},
    {headerName:"Late",  field: "In Time",width:"80px",filter:true},
    {headerName:"Absent",  field: "Absent",width:"150px",filter:true},
    {headerName:"Enroll No.",  field: "Enroll No.",width:"80px",filter:true},
    {headerName:"Father's Name",  field: "Father's Name",width:"200px",filter:true},
    {headerName:"Father Contact No.",  field: "Contact No",width:"150px",filter:true},
  
    
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
var eGridDivleft = document.querySelector('#myGridlast');


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
   
	{headerName:"Id", field: "Id",filter:true, width:"80px",filter: "agNumberColumnFilter"},
	{headerName:"Regd. No." ,field: "Roll No.",width:"130px",filter: "agNumberColumnFilter"},
    {headerName:"Class/Section",field: "Class",width:"150px",filter:true},
    {headerName:"Roll No." ,field: "Roll No.",width:"130px",filter: "agNumberColumnFilter"},
    {headerName:"Name", field: "Name",width:"200px",filter:'agTextColumnFilter' },
    {headerName:"Contact No.",  field: "Contact No",width:"150px",filter:true},
    {headerName:"Parent's Name",  field: "Parent's Name",width:"200px",filter:true},
    {headerName:"Address",  field: "Address",width:"150px",filter:true},
    {headerName:"Attendance",  field: "Attendance",width:"150px",filter:true},
   
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
var eGridDivmedium = document.querySelector('#myGrid-daily');


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
   
    {headerName:"Id", field: "Id",filter:true, width:"80px",filter: "agNumberColumnFilter"},
	{headerName:"Regd. No." ,field: "Roll No.",width:"130px",filter: "agNumberColumnFilter"},
    {headerName:"Roll No." ,field: "Roll No.",width:"130px",filter: "agNumberColumnFilter"},
    {headerName:"Name", field: "Name",width:"200px",filter:'agTextColumnFilter' },
    {headerName:"Class",field: "Class",width:"100px",filter:true},
   
    {headerName:"Present",  field: "Present",width:"150px",filter:true},
    {headerName:"Absent",  field: "Absent",width:"150px",filter:true},
   
    {headerName:"Late",  field: "In Time",width:"80px",filter:true},
    {headerName:"Leave",  field: "Absent",width:"150px",filter:true},
   
    {headerName:"Father's Name",  field: "Father's Name",width:"200px",filter:true},
    {headerName:"Father Contact No.",  field: "Contact No",width:"150px",filter:true},
    
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
var eGridDivshift = document.querySelector('#myGridsummary');


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

