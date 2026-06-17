// Transport Student List
var columnDefs = [
    
	{headerName:"Id", field: "Id",filter:true, width:"80px",filter: "agNumberColumnFilter"},
	{headerName:"Regd. No." ,field: "Roll No.",width:"130px",filter: "agNumberColumnFilter"},
    {headerName:"Account",  field: "Account",width:"150px",filter:true},
    {headerName:"Roll No." ,field: "Roll No.",width:"130px",filter: "agNumberColumnFilter"},
    {headerName:"Name", field: "Name",width:"200px",filter:'agTextColumnFilter' },
    {headerName:"Class",field: "Class",width:"100px",filter:true},
    {headerName:"Transport Point",  field: "Transport Point",width:"150px",filter:true},
    {headerName:"Transport Route",  field: "Transport Route",width:"150px",filter:true},
    {headerName:"Fee Amt.",  field: "Fee Amt.",width:"80px",filter:true},
    {headerName:"Paid Amt.",  field: "Paid Amt.",width:"80px",filter:true},
    {headerName:"Balance Amt.",  field: "Balance Amt.",width:"130px",filter:true},
   
    {headerName:"Father's Name",  field: "Father's Name",width:"200px",filter:true},
    {headerName:"Contact No.",  field: "Contact No",width:"150px",filter:true},
    {headerName:"Address",  field: "Address",width:"100px",filter:true},
    {headerName:"Is Left",  field: "Is Left",width:"100px",filter:true},
    {headerName:"Left Date",  field: "Left Date",width:"100px",filter:true},
  
    
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


// Transport Student List for Month

var columnDefs = [
   
	{headerName:"Id", field: "Id",filter:true, width:"80px",filter: "agNumberColumnFilter"},
	{headerName:"Regd. No." ,field: "Roll No.",width:"130px",filter: "agNumberColumnFilter"},
    {headerName:"Account",  field: "Account",width:"150px",filter:true},
    {headerName:"Roll No." ,field: "Roll No.",width:"130px",filter: "agNumberColumnFilter"},
    {headerName:"Name", field: "Name",width:"200px",filter:'agTextColumnFilter' },
    {headerName:"Class",field: "Class",width:"100px",filter:true},
    {headerName:"Transport Point",  field: "Transport Point",width:"150px",filter:true},
    {headerName:"Transport Route",  field: "Transport Route",width:"150px",filter:true},
    {headerName:"Rate",  field: "Rate",width:"80px",filter:true},
    {headerName:"Discount Amt.",  field: "Discount Amt.",width:"180px",filter:true},
    {headerName:"Payable Amt.",  field: "Payable Amt.",width:"130px",filter:true},
   
    {headerName:"Father's Name",  field: "Father's Name",width:"200px",filter:true},
    {headerName:"Contact No.",  field: "Contact No",width:"150px",filter:true},
    {headerName:"Address",  field: "Address",width:"100px",filter:true},
    {headerName:"Is Left",  field: "Is Left",width:"100px",filter:true},
    {headerName:"Left Date",  field: "Left Date",width:"100px",filter:true},
    
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




