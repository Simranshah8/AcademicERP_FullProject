// Fee Module ->Additional->discount-type-wise-student-list

var columnDefs = [  
    {headerName:"Id", field: "Id" ,width:100,filter: "agNumberColumnFilter" },
    {headerName:"Regd No.", field: "Regd No.", width:100,filter: "agNumberColumnFilter" },
      {headerName:"Roll No.", field: "Roll No.",width:100,filter: "agNumberColumnFilter"  },
      {headerName:"Name", field: "Name",width:250,filter:'agTextColumnFilter' },
      {headerName:"Class", field: "Class",width:200 ,filter:'agTextColumnFilter'},
      {headerName:"Discount Type", field: "Discount Type",width:200 },
      {headerName:"Father Name", field: "Father Name",width:200,filter:'agTextColumnFilter' },
      {headerName:"Contact No.", field: "Contact No." ,width:150,filter: "agNumberColumnFilter" },
      {headerName:"Address", field: "Address" ,width:200,filter:'agTextColumnFilter'},
      {headerName:"Transport Point", field: "Transport Point",width:150,filter:'agTextColumnFilter' },
      {headerName:"Boarders Name", field: "Boarders Name" ,width:150,filter:'agTextColumnFilter'},
      {headerName:"House Name", field: "House Name" ,width:150,filter:'agTextColumnFilter'},
      {headerName:"Remarks", field: "Remarks" ,width:250,filter:'agTextColumnFilter'},
      {headerName:"Is Left",  field: "Is Left",width:100 },
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
    minWidth:100,
    rowSelection: 'multiple',
    
    },
    };
    var eGridDivAdditional = document.querySelector('#discount-type-wise-student-list');
    new agGrid.Grid(eGridDivAdditional, gridOptionsAdditional);
    var dataCollAdditional=[];
    dataCollAdditional.push({
      
    Name:'Ram',
    
    });
    gridOptionsAdditional.api.setRowData(dataCollAdditional);
    

    // Fee Module ->Additional->fee-heading-wise-student-list-only

var columnDefs = [
  
    {headerName:"Id", field: "Id" ,width:100,filter: "agNumberColumnFilter" },
    {headerName:"Regd No.", field: "Regd No.", width:100,filter: "agNumberColumnFilter" },
      {headerName:"Roll No.", field: "Roll No.",width:100 ,filter: "agNumberColumnFilter" },
      {headerName:"Name", field: "Name",width:250,filter:'agTextColumnFilter' },
      {headerName:"Class", field: "Class",width:200,filter:'agTextColumnFilter' },
      {headerName:"Fee Item", field: "Fee Item",width:150 },
      {headerName:"Discount Amt.", field: "Discount Amt.",width:200,filter: "agNumberColumnFilter"  },
      {headerName:"Discount %", field: "Discount %" ,width:150,filter: "agNumberColumnFilter" },
      {headerName:"Father Name", field: "Father Name",width:200 ,filter:'agTextColumnFilter'},
      {headerName:"Contact No.", field: "Contact No.",width:150 ,filter: "agNumberColumnFilter" },
      {headerName:"Address", field: "Address" ,width:200,filter:'agTextColumnFilter'},
      {headerName:"Transport Point", field: "Transport Point",width:150 ,filter:'agTextColumnFilter'},
      {headerName:"Boarders Name", field: "Boarders Name" ,width:150,filter:'agTextColumnFilter'},
      {headerName:"House Name", field: "House Name" ,width:150,filter:'agTextColumnFilter'},
      {headerName:"Remarks", field: "Remarks" ,width:250,filter:'agTextColumnFilter'},
      {headerName:"Is Left",  field: "Is Left",width:100 },
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
    minWidth:100,
    rowSelection: 'multiple'
    },
    };
    var eGridDivAdditional = document.querySelector('#fee-heading-wise-student-list-only');
    new agGrid.Grid(eGridDivAdditional, gridOptionsAdditional);
    var dataCollAdditional=[];
    dataCollAdditional.push({
      
    Name:'Ram',
    
    });
    gridOptionsAdditional.api.setRowData(dataCollAdditional);


     // Fee Module ->Additional->fee-heading-wise-student-list

var columnDefs = [
  
    {headerName:"Id", field: "Id" ,width:100,filter: "agNumberColumnFilter" },
    {headerName:"Regd No.", field: "Regd No.", width:100,filter: "agNumberColumnFilter" },
      {headerName:"Roll No.", field: "Roll No.",width:100 ,filter: "agNumberColumnFilter" },
      {headerName:"Name", field: "Name",width:250,filter:'agTextColumnFilter' },
      {headerName:"Class", field: "Class",width:200 ,filter:'agTextColumnFilter'},
      {headerName:"Fee Item", field: "Fee Item",width:150 },
      {headerName:"Discount Amt.", field: "Discount Amt.",width:200 ,filter: "agNumberColumnFilter" },
      {headerName:"Discount %", field: "Discount %" ,width:150},
      {headerName:"Father Name", field: "Father Name",width:200,filter:'agTextColumnFilter' },
      {headerName:"Contact No.", field: "Contact No.",width:150 ,filter: "agNumberColumnFilter" },
      {headerName:"Address", field: "Address" ,width:200},
      {headerName:"Transport Point", field: "Transport Point",width:150 },
      {headerName:"Boarders Name", field: "Boarders Name" ,width:150,filter:'agTextColumnFilter'},
      {headerName:"House Name", field: "House Name" ,width:150,filter:'agTextColumnFilter'},
      {headerName:"Remarks", field: "Remarks" ,width:250,filter:'agTextColumnFilter'},
      {headerName:"Is Left",  field: "Is Left",width:100 },
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
    minWidth:100,
    rowSelection: 'multiple'
    },
    };
    var eGridDivAdditional = document.querySelector('#fee-heading-wise-student-list');
    new agGrid.Grid(eGridDivAdditional, gridOptionsAdditional);
    var dataCollAdditional=[];
    dataCollAdditional.push({
      
    Name:'Ram',
    
    });
    gridOptionsAdditional.api.setRowData(dataCollAdditional);
    
    // Fee Module ->Additional->student-wise-discount-section

var columnDefs = [
  
    {headerName:"Id", field: "Id" ,width:100,filter: "agNumberColumnFilter" },
    {headerName:"Regd No.", field: "Regd No.", width:100,filter: "agNumberColumnFilter" },
      {headerName:"Roll No.", field: "Roll No.",width:100,filter: "agNumberColumnFilter"  },
      {headerName:"Name", field: "Name",width:250 ,filter:'agTextColumnFilter'},
      {headerName:"Class", field: "Class",width:200 ,filter:'agTextColumnFilter'},
      {headerName:"Previous Dues", field: "Previous Dues",width:150 ,filter: "agNumberColumnFilter" },
      {headerName:"Current Fee", field: "Current Fee",width:200 ,filter: "agNumberColumnFilter" },
      {headerName:"Paid Amt.", field: "Paid Amt." ,width:150 ,filter: "agNumberColumnFilter" },
      {headerName:"Discount Amt.", field: "Discount Amt.",width:200,filter: "agNumberColumnFilter"  },
      {headerName:"Balance Amt.", field: "Balance Amt.",width:150,filter: "agNumberColumnFilter"  },
     
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
    width:100,
    rowSelection: 'multiple'
    },
    };
    var eGridDivAdditional = document.querySelector('#student-wise-discount-section');
    new agGrid.Grid(eGridDivAdditional, gridOptionsAdditional);
    var dataCollAdditional=[];
    dataCollAdditional.push({
      
    Name:'Ram',
    
    });
    gridOptionsAdditional.api.setRowData(dataCollAdditional);
    
   
    
    