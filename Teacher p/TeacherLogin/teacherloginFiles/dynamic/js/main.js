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




// quick access js
on_load =()=>{
  $('.remarks , .complain , .notification , .assignment , .exam-area , .fee , .e-library , .document').hide();
  //  $('.period-wise , .exam-wise , .leave-wise').hide();
}
window.onload=on_load();
$(document).ready(function() {

  $('.card-1').click(function(){
   $('.attendance').show();
   $('.remarks , .complain , .notification , .assignment , .exam-area , .fee , .e-library , .document').hide();
  //  $('.period-wise , .exam-wise , .leave-wise').hide();
   $('.card-1').addClass('current-page');
   $('.card-2 , .card-3 , .card-4 , .card-5 , .card-6 , .card-7 , .card-8 , .card-9').removeClass('current-page');
  });
  
  $('.card-2').click(function(){
    $('.remarks').show();
    $('.attendance , .complain , .notification , .assignment , .exam-area , .fee , .e-library , .document').hide();
    $('.card-2').addClass('current-page');
    $('.card-1 , .card-3 , .card-4 , .card-5 , .card-6 , .card-7 , .card-8 , .card-9').removeClass('current-page');
   });
  
   $('.card-3').click(function(){
    $('.complain').show();
    $('.attendance , .remarks , .notification , .assignment , .exam-area , .fee , .e-library , .document').hide();
    $('.card-3').addClass('current-page');
    $('.card-1 , .card-2 , .card-4 , .card-5 , .card-6 , .card-7 , .card-8 , .card-9').removeClass('current-page');
   });
  
   $('.card-4').click(function(){
    $('.notification').show();
    $('.attendance , .remarks , .complain , .assignment , .exam-area , .fee , .e-library , .document').hide();
    $('.card-4').addClass('current-page');
    $('.card-1 , .card-2 , .card-3 , .card-5 , .card-6 , .card-7 , .card-8 , .card-9').removeClass('current-page');
   });
  
   $('.card-5').click(function(){
    $('.assignment').show();
    $('.attendance , .remarks , .complain , .notification , .exam-area , .fee , .e-library , .document').hide();
    $('.card-5').addClass('current-page');
    $('.card-1 , .card-2 , .card-3 , .card-4 , .card-6 , .card-7 , .card-8 , .card-9').removeClass('current-page');
   });
  
   $('.card-6').click(function(){
    $('.exam-area').show();
    $('.attendance , .remarks , .complain , .notification , .assignment , .fee , .e-library , .document').hide();
    $('.card-6').addClass('current-page');
    $('.card-1 , .card-2 , .card-3 , .card-4 , .card-5 , .card-7 , .card-8 , .card-9').removeClass('current-page');
   });
  
   $('.card-7').click(function(){
    $('.fee').show();
    $('.attendance , .remarks , .complain , .notification , .assignment , .exam-area , .e-library , .document').hide();
    $('.card-7').addClass('current-page');
    $('.card-1 , .card-2 , .card-3 , .card-4 , .card-5 , .card-6 , .card-8 , .card-9').removeClass('current-page');
   });
  
   $('.card-8').click(function(){
    $('.e-library').show();
    $('.attendance , .remarks , .complain , .notification , .assignment , .exam-area , .fee , .document').hide();
    $('.card-8').addClass('current-page');
    $('.card-1 , .card-2 , .card-3 , .card-4 , .card-5 , .card-6 , .card-7 , .card-9').removeClass('current-page');
   });
  
   $('.card-9').click(function(){
    $('.document').show();
    $('.attendance , .remarks , .complain , .notification , .assignment , .exam-area , .fee , .e-library').hide();
    $('.card-9').addClass('current-page');
    $('.card-1 , .card-2 , .card-3 , .card-4 , .card-5 , .card-6 , .card-7 , .card-8').removeClass('current-page');
   });
  
  
  //  attendance section start
  
      // $('.daily').click(function(){
      //   $('.daily-wise').show();
      //   $('.period-wise , .exam-wise , .leave-wise').hide();
      //   $('.remarks , .complain , .notification , .assignment , .exam-area , .fee , .e-library , .document').hide();
      //   $('.daily').addClass('active-sheet-daily');
      //   $('.period , .exam , .leave').removeClass('active-sheet');
      // });
  
      // $('.period').click(function(){
      //   $('.period-wise').show();
      //   $('.daily-wise , .exam-wise , .leave-wise').hide();
      //   $('.remarks , .complain , .notification , .assignment , .exam-area , .fee , .e-library , .document').hide();
      //   $('.period').addClass('active-sheet');
      //   $('.daily').removeClass('active-sheet-daily');
      //   $('.exam , .leave').removeClass('active-sheet');
      // });
  
      // $('.exam').click(function(){
      //   $('.exam-wise').show();
      //   $('.daily-wise , .period-wise , .leave-wise').hide();
      //   $('.remarks , .complain , .notification , .assignment , .exam-area , .fee , .e-library , .document').hide();
      //   $('.exam').addClass('active-sheet');
      //   $('.daily').removeClass('active-sheet-daily');
      //   $('.period , .leave').removeClass('active-sheet');
      // });
  
      // $('.leave').click(function(){
      //   $('.leave-wise').show();
      //   $('.daily-wise , .period-wise , .exam-wise').hide();
      //   $('.remarks , .complain , .notification , .assignment , .exam-area , .fee , .e-library , .document').hide();
      //   $('.leave').addClass('active-sheet');
      //   $('.daily').removeClass('active-sheet-daily');
      //   $('.period , .exam').removeClass('active-sheet');
      // });
  
  // attendance section closed
  
  
  // assignment buttons
  $('.submited').click(function(){
    $('.submited-table').show();
    $('.not-submited-table').hide();
    $('.attendance , .remarks , .complain , .notification , .exam-area , .fee , .e-library , .document').hide();
    $('.submited').addClass('active');
    $('.not-submited').removeClass('active');
  });
  
  $('.not-submited').click(function(){
    $('.not-submited-table').show();
    $('.submited-table').hide();
    $('.attendance , .remarks , .complain , .notification , .exam-area , .fee , .e-library , .document').hide();
    $('.not-submited').addClass('active');
    $('.submited').removeClass('active');
  });
  
  
  // fee buttons
      $('.ledger').click(function() {
          
          $('.student-ledger').show();
          $('.student-voucher').hide();
          $('.attendance , .remarks , .complain , .notification , .assignment , .exam-area , .e-library , .document').hide();
          $('.ledger').addClass("active-sheet");
          $('.voucher').removeClass("active-sheet");
      });
  
      $('.voucher').click(function(){
        $('.student-voucher').show();
        $('.student-ledger').hide();
        $('.attendance , .remarks , .complain , .notification , .assignment , .exam-area , .e-library , .document').hide();
        $('.voucher').addClass("active-sheet");
        $('.ledger').removeClass("active-sheet");
      });
  
      // exam section btns
  
      $('.result-section').click(function(){
        $('.result-evaluation-section').show();
        $('.exam-summery-section').hide();
        $('.subject-evaluation-section').hide();
        $('.result-section').addClass('active-page');
        $('.subject-section').removeClass('active-page');
      });
  
      $('.subject-section').click(function(){
        $('.subject-evaluation-section').show();
        $('.exam-summery-section').hide();
        $('.result-evaluation-section').hide();
        $('.subject-section').addClass('active-page');
        $('.result-section').removeClass('active-page');
      });
  
     
  });


