// class wise
        var columnDefs = [
   
	
	{headerName:"Days/Period", field: "DaysPeriod",width:400 },
    {headerName:"Period Name" ,field: "Period Name",width:850 },
    
  ];

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
var eGridDiv = document.querySelector('#schedule-report-table');


// create the grid passing in the div to use together with the columns & data we want to use
new agGrid.Grid(eGridDiv, gridOptions);
var dataColl=[];
dataColl.push({
	DaysPeriod:"Day's Name",
	
});


gridOptions.api.setRowData(dataColl);
  

// class schedule
var columnDefs = [
   
	
	{headerName:"Class/Period", field: "Period",width:400 },
    {headerName:"Period Name" ,field: "Period Name",width:850 },
    
  ];

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
var eGridDiv = document.querySelector('#class-schedule-table');


// create the grid passing in the div to use together with the columns & data we want to use
new agGrid.Grid(eGridDiv, gridOptions);
var dataColl=[];
dataColl.push({
	Period:"Class Name",
	
});


gridOptions.api.setRowData(dataColl);
  

// teacher schedule
var columnDefs = [
   
	
	{headerName:"Days/Period", field: "DaysPeriod",width:400 },
    {headerName:"Period Name" ,field: "Period Name",width:850 },
    
  ];

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
var eGridDiv = document.querySelector('#teacher-schedule-table');


// create the grid passing in the div to use together with the columns & data we want to use
new agGrid.Grid(eGridDiv, gridOptions);
var dataColl=[];
dataColl.push({
	DaysPeriod:"Day's Name",
	
});


gridOptions.api.setRowData(dataColl);


// free teacher
var columnDefs = [
   
	
	{headerName:"Days/Period", field: "DaysPeriod",width:400 },
    {headerName:"Period Name" ,field: "Period Name",width:850 },
    
  ];

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
var eGridDiv = document.querySelector('#vacant-table');


// create the grid passing in the div to use together with the columns & data we want to use
new agGrid.Grid(eGridDiv, gridOptions);
var dataColl=[];
dataColl.push({
	DaysPeriod:"Day's Name",
	
});


gridOptions.api.setRowData(dataColl);