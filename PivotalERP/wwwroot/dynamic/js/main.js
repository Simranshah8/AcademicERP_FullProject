// For Tooltip
$(function () {
    $('[data-toggle="tooltip"]').tooltip()
})


  // The Calender
  // $('#calendar').datetimepicker({
  //   format: 'L',
  //   inline: true
  // })




    // jQuery UI sortable for the todo list

  // $(function () {
  //   $('.todo-list').sortable({
  //       placeholder: 'sort-highlight',
  //       handle: '.handle',
  //       forcePlaceholderSize: true,
  //       zIndex: 999999
  //   })
  // })


  // For fixed height width table scroll which is used in Sales-Invoice page and modal
$(function() {
  //The passed argument has to be at least a empty object or a object with your desired options
  $('.scroll-only-y').overlayScrollbars({
      scrollbars: {
          autoHide: "scroll",
          autoHide: "never",

      },
  });

});


// For fixed height width table scroll which is used in Sales-Invoice page and modal
$(function() {
  //The passed argument has to be at least a empty object or a object with your desired options
  $('.table-scroll-x-y').overlayScrollbars({
      scrollbars: {
          autoHide: "scroll",
          autoHide: "never",

      },
  });

});


// For Calendar used in sales-invoice
$(function() {
  $('input[name="DatePickerOnly"]').daterangepicker({
      singleDatePicker: true,
      showDropdowns: true,
      minYear: 1901,
      maxYear: parseInt(moment().format('YYYY'), 10)
  });
});


// Student mapping table 

function ChangeDropdowns(value){
  if(value=="th"){
  document.getElementById('code-pr').style.display='none';
  document.getElementById('data-pr').style.display='none';
  document.getElementById('data-th').style.display='block';
  document.getElementById('code-th').style.display='block';
  }else if(value=="pr"){
    document.getElementById('code-th').style.display='none';
    document.getElementById('data-th').style.display='none';
    document.getElementById('data-pr').style.display='block';
    document.getElementById('code-pr').style.display='block';
  }
else if(value=="both"){
  document.getElementById('main-table').rows[0].cells.length;
  document.getElementById('code-th').style.display='block';
  document.getElementById('code-pr').style.display='block';
  document.getElementById('data-pr').style.display='block';
  document.getElementById('data-th').style.display='block';
  }
  else if(value=="grading"){
    document.getElementById('data-pr').style.display='none';
    document.getElementById('data-th').style.display='none';
    document.getElementById('code-th').style.display='none';
    document.getElementById('code-pr').style.display='none';
    }
    
}