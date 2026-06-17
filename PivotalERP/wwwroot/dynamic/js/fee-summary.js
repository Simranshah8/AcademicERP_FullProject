// Fee Summary Table Starts
var columnDefs = [
    { headerName: "Id", field: "Id",filter: "agNumberColumnFilter",width:100},
    { headerName: "Regd. No", field: "regd",filter: "agNumberColumnFilter",width:150},
    { headerName: "Roll No.", field: "rollno",filter: "agNumberColumnFilter",width:150},
    { headerName: "Name", field: "Name",width:200,filter:'agTextColumnFilter'},
    { headerName: "Class/Sec", field: "class",width:150,filter:'agTextColumnFilter'},
    { headerName: "Previous Dues", field: "previous",width:150},
    { headerName: "Current Fee", field: "currentfee",width:150},
    { headerName: "Paid Amount", field: "paid",filter: "agNumberColumnFilter", width:150},
    { headerName: "Discount Amount", field: "discount",filter: "agNumberColumnFilter",width:150},
    { headerName: "Balance Amount", field: "balance",filter: "agNumberColumnFilter",width:150},
    { headerName: "Father Name", field: "fathername",width:150,filter:'agTextColumnFilter'},
    { headerName: "Contact No.", field: "contact",width:150},
    { headerName: "Address", field: "address",width:150,filter:'agTextColumnFilter'},
    { headerName: "Is left", field: "isleft",width:150},
    { headerName: "Left Date", field: "leftdate",width:150},
    { headerName: "Transport Point", field: "transportpoint",width:150},
    { headerName: "Transport Route", field: "transportroute",width:150},
    { headerName: "Boarders Name", field: "boardersname",width:150,filter:'agTextColumnFilter'},
    { headerName: "D.O.B.(A.D.)", field: "dobbs",width:150},
    { headerName: "D.O.B.(B.S.)", field: "dobad",width:150},  
  ];
  var gridOptionsfees = {
    columnDefs: columnDefs,
    headerHeight:31,
    rowHeight:31,
    enableSorting: true,
    multiSortKey: 'ctrl',
    enableColResize: true,
    overlayLoadingTemplate : "Loading..",
    overlayNoRowsTemplate : "No Records found",
    rowSelection: 'multiple',
    
    rowData: null,
    filter: true,
    suppressHorizontalScroll: false,
    alignedGrids: [],
    enableFilter: true,
  
    defaultColDef:{
      sortable :true,
      filter:true,
      resizable: true,
      cellStyle:{'line-height':'31px'},
      rowSelection: 'multiple'
    
    }
    };
    
    var eGridDivfees = document.querySelector('#myGridfees');
    // create the grid passing in the div to use together with the columns & data we want to use
    new agGrid.Grid(eGridDivfees, gridOptionsfees);
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
    
    gridOptionsfees.api.setRowData(dataColl6);
  // Fee Summary Table Ends
  
  // for bottom row
  dataForBottomGrid = [
    {
        Id:'',
        regd: 'Total =>',
       
        rollno:0,
        Name:'',
        Class:'',
        previous:0,
        currentfee:0,
        paid:0,
        discount:0,
        balance:0,
        fathername:'',
        contact:'',
        address:'',
        isleft:'',
        leftdate:'',
        transportpoint:'',
        transportroute: '',
        boardersname:'',
        dobbs:'',
        dobad:''
    }
  ];
  
   var gridOptionsBottom = {
    defaultColDef: {
        resizable: true,
        width: 90
    },
    columnDefs: columnDefs,
    // we are hard coding the data here, it's just for demo purposes
    rowData: dataForBottomGrid,
    debug: true,
    rowClass: 'bold-row',
    // hide the header on the bottom grid
    headerHeight: 20,
    alignedGrids: []
  };
  
  gridOptions.alignedGrids.push(gridOptionsBottom);
  gridOptionsBottom.alignedGrids.push(gridOptions);
  
  gridDivBottom = document.querySelector('#myGridBottom');
  new agGrid.Grid(gridDivBottom, gridOptionsBottom);
  
  // gridOptionsBottom.api.setRowData(dataForBottomGrid);