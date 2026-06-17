// Fee Item Wise Summary Table Starts
var columnDefs = [
    { headerName: "Id", field: "Id",width:150},
    { headerName: "Regd. No", field: "regd",width:150},
    { headerName: "Roll No.", field: "rollno",width:150},
    { headerName: "Name", field: "Name",width:250},
    { headerName: "Class", field: "class",width:150},
    { headerName: "Fee Amount", field: "feeamt",width:150},   
    { headerName: "Paid Amount", field: "paid",width:150},
    { headerName: "Dues Amount", field: "dues",width:150},
    { headerName: "Rate", field: "rate",width:150},
    { headerName: "Father Name", field: "fathername",width:250},
    { headerName: "Contact No.", field: "contact",width:150},
    { headerName: "Address", field: "address",width:250},
    { headerName: "Is left", field: "isleft",width:150},
    { headerName: "Left Date", field: "leftdate",width:150},
    { headerName: "Transport Point", field: "transportpoint",width:150},    
    { headerName: "Boarders Name", field: "boardersname",width:150},
   
  ];
  var gridOptionsfeeitemsummary = {
    columnDefs: columnDefs,
    headerHeight:31,
    rowHeight:31,
    defaultColDef:{
      sortable :true,
      filter:true,
      resizable: true,
      cellStyle:{'line-height':'31px'},
      rowSelection: 'multiple'
    
    }
    };
    
    var eGridDivfeeitemsummary = document.querySelector('#myGridfeeitemsummary');
    // create the grid passing in the div to use together with the columns & data we want to use
    new agGrid.Grid(eGridDivfeeitemsummary, gridOptionsfeeitemsummary);
    var dataColl7=[];
    dataColl7.push({
    Id:111,
    Name:'Gopal',
    RollNo :'1233',
    });
    dataColl7.push({
    Id:111,
    Name:'Rohit',
      RollNo :'1233',
    });
    
    gridOptionsfeeitemsummary.api.setRowData(dataColl7);
 // Fee Item Wise Summary Table Ends


 // Fee Summary With Nominal Table Starts
 var columnDefs = [
    { headerName: "Id", field: "Id",filter: "agNumberColumnFilter",width:150},
    { headerName: "Regd. No", field: "regd",filter: "agNumberColumnFilter",width:150},
    { headerName: "Roll No.", field: "rollno",filter: "agNumberColumnFilter",width:150},
    { headerName: "Name", field: "Name",width:250},
    { headerName: "Class", field: "class",width:150},
    { headerName: "Previous Dues", field: "predues",width:150},
    { headerName: "Current Fee", field: "currentfee",filter: "agNumberColumnFilter",width:150},   
    { headerName: "Paid Amount", field: "paid",filter: "agNumberColumnFilter",width:150},
    { headerName: "Discount Amount", field: "discount",filter: "agNumberColumnFilter",width:150},
    { headerName: "Balance Amount", field: "discount",filter: "agNumberColumnFilter",width:150},
    { headerName: "Deposit Amount", field: "discount",filter: "agNumberColumnFilter",width:150},
    { headerName: "Grad Total", field: "rate",filter: "agNumberColumnFilter",width:150},
    { headerName: "Father Name", field: "fathername",width:250},
    { headerName: "Contact No.", field: "contact",width:150},
    { headerName: "Address", field: "address",width:250},
    { headerName: "Is left", field: "isleft",width:150},
    { headerName: "Left Date", field: "leftdate",width:150},
    { headerName: "Transport Point", field: "transportpoint",width:150},    
    { headerName: "Boarders Name", field: "boardersname",width:150},
    { headerName: "D.B.O.(A.D.)", field: "boardersname",width:150},
    { headerName: "D.B.O.(B.S.)", field: "boardersname",width:150},
   
  ];
  var gridOptionsfeeitemnominal = {
    columnDefs: columnDefs,
    headerHeight:31,
    rowHeight:31,
    defaultColDef:{
      sortable :true,
      filter:true,
      resizable: true,
      cellStyle:{'line-height':'31px'},
      rowSelection: 'multiple'
    
    }
    };
    
    var eGridDivfeeitemnominal = document.querySelector('#myGridfeeitemnominal');
    // create the grid passing in the div to use together with the columns & data we want to use
    new agGrid.Grid(eGridDivfeeitemnominal, gridOptionsfeeitemnominal);
    var dataColl7=[];
    dataColl7.push({
    Id:111,
    Name:'Gopal',
    RollNo :'1233',
    });
    dataColl7.push({
    Id:111,
    Name:'Rohit',
      RollNo :'1233',
    });
    
    gridOptionsfeeitemnominal.api.setRowData(dataColl7);
  // Fee Summary With Nominal Table Starts