// Fee Receipt Table Starts
var columnDefs = [
    { headerName: "S.No.", field: "sn",filter: "agNumberColumnFilter",width:100},
    { headerName: "Fee Item", field: "feeitem",width:150},    
    { headerName: "Previous Dues", field: "previousdues",filter: "agNumberColumnFilter",width:150},
    { headerName: "Current Dues", field: "currentdues",filter: "agNumberColumnFilter",width:150},
    { headerName: "Total Dues", field: "totaldues",filter: "agNumberColumnFilter",width:150},
    { headerName: "Discount %", field: "discount",filter: "agNumberColumnFilter",width:150},
    { headerName: "Discount Amount", field: "discountamt",filter: "agNumberColumnFilter",width:200},
    { headerName: "Receivable Amount", field: "receivable",filter: "agNumberColumnFilter",width:200},
    { headerName: "More Amount", field: "moreamt",filter: "agNumberColumnFilter",width:150},
    { headerName: "Received Amount", field: "received",filter: "agNumberColumnFilter",width:200},
    { headerName: "After Receiving Dues", field: "afterrecdues",filter: "agNumberColumnFilter",width:250},
   
  ];
  var gridOptionsfeesreceipt = {
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
    
    var eGridDivfeesreceipt = document.querySelector('#myGridfeesreceipt');
    // create the grid passing in the div to use together with the columns & data we want to use
    new agGrid.Grid(eGridDivfeesreceipt, gridOptionsfeesreceipt);
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
    
    gridOptionsfeesreceipt.api.setRowData(dataColl6);
  // Fee Receipt Table Ends


  // Fee Receipt Collection Starts
var columnDefs = [
    { headerName: "S.No.", field: "sn",filter: "agNumberColumnFilter",width:80},
    { headerName: "Date of Birth", field: "dob",width:150},    
    { headerName: "Month Name", field: "month",width:150},
    { headerName: "Name", field: "name",width:150},
    { headerName: "ID", field: "id",filter: "agNumberColumnFilter",width:80},
    { headerName: "Regd.No.", field: "regd",filter: "agNumberColumnFilter",width:150},
    { headerName: "Roll No.", field: "roll",filter: "agNumberColumnFilter",width:150},
    { headerName: "Class", field: "class",filter: "agNumberColumnFilter",width:150},
    { headerName: "Ref.No.", field: "ref",filter: "agNumberColumnFilter",width:150},
    { headerName: "Rec.No.", field: "rec",filter: "agNumberColumnFilter",width:150},
    { headerName: "Amount", field: "amount",filter: "agNumberColumnFilter",width:150},
    { headerName: "Discount Amt.", field: "discountamt",filter: "agNumberColumnFilter",width:150}, 
    { headerName: "More Amt.", field: "moreamt",filter: "agNumberColumnFilter",width:150}, 
    { headerName: "Tax Amt.", field: "taxamt",filter: "agNumberColumnFilter",width:150},
    { headerName: "Father's Name", field: "father",width:200},
    { headerName: "Contact No.", field: "contact",width:150},
    { headerName: "Address", field: "address",width:200},
    { headerName: "User", field: "user",width:200}, 
    { headerName: "Action", field: "action",width:100},         
  ];
  var gridOptionsfeescollection = {
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
    
    var eGridDivfeescollection = document.querySelector('#myGridfeescollection');
    // create the grid passing in the div to use together with the columns & data we want to use
    new agGrid.Grid(eGridDivfeescollection, gridOptionsfeescollection);
    var dataColl6=[];
    dataColl6.push({
    sn:111,
    feeitem:'Admission Fee',
    
    });
    dataColl6.push({
    sn:111,
    feeitem:'Hostel Fee',
    });
    
    gridOptionsfeescollection.api.setRowData(dataColl6);
  // Fee Receipt Collection Ends


  
  // Online Payment Collection Starts
var columnDefs = [
    { headerName: "S.No.", field: "sn",width:100},
    { headerName: "Date(B.S.)", field: "date",width:150},    
    ,
    { headerName: "Gateway", field: "gateway",width:120},
    { headerName: "ID", field: "id",width:80},
    { headerName: "Regd.No.", field: "regd",width:120},
    { headerName: "Roll No.", field: "rollno",width:120},
    { headerName: "Class", field: "class",width:150},
    { headerName: "Txn Id", field: "txnid",width:150},
    
    { headerName: "Amount", field: "amount",width:150},         
  ];
  var gridOptionsonlinepayment = {
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
    
    var eGridDivonlinepayment = document.querySelector('#myGridonlinepayment');
    // create the grid passing in the div to use together with the columns & data we want to use
    new agGrid.Grid(eGridDivonlinepayment, gridOptionsonlinepayment);
    var dataColl6=[];
    dataColl6.push({
    sn:111,
    feeitem:'Admission Fee',
    
    });
    dataColl6.push({
    sn:111,
    feeitem:'Hostel Fee',
    });
    
    gridOptionsonlinepayment.api.setRowData(dataColl6);
  // Online Payment Collection Ends