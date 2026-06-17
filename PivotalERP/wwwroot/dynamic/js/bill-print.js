// Fee Summary Table Starts
var columnDefs = [
    { headerName: "Id", field: "Id",width:100},
    { headerName: "Regd. No", field: "regd",width:150},
    { headerName: "Roll No.", field: "rollno",width:150},
    { headerName: "Name", field: "Name",width:200},
    { headerName: "Class/Sec", field: "class",width:150},
    { headerName: "Fee Amount", field: "fee",width:150},   
    { headerName: "Paid Amount", field: "paid",width:150},
    { headerName: "Dues Amount", field: "dues",width:150},    
    { headerName: "Father Name", field: "fathername",width:150},
    { headerName: "Father Contact No.", field: "fathercontact",width:250},
    { headerName: "Address", field: "address",width:150},
    { headerName: "Is left", field: "isleft",width:150},
    { headerName: "Left Date", field: "leftdate",width:150},
    { headerName: "Transport Point", field: "transportpoint",width:150},   
    { headerName: "Boarders Name", field: "boardersname",width:150},
    
  ];
  var gridOptionsbillprint = {
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
    
    var eGridDivbillprint = document.querySelector('#myGridbillprint');
    // create the grid passing in the div to use together with the columns & data we want to use
    new agGrid.Grid(eGridDivbillprint, gridOptionsbillprint);
    var dataColl6=[];
    dataColl6.push({
    Id:111,
    Name:'Gopal',
    RollNo :'1233',
    });
    dataColl6.push({
    Id:111,
    Name:'Rohit',
      RollNo :'1233',
    });
    
    gridOptionsbillprint.api.setRowData(dataColl6);
  // Fee Summary Table Ends