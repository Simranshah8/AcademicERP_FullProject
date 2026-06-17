on_load =()=>{
    document.getElementById('exam-details').style.display="none";
    document.getElementById('exam-document').style.display="none";
    document.getElementById('exam-questions').style.display="none";
    document.getElementById('exam-questions-2').style.display="none";
    document.getElementById('question-summary').style.display="none";
    document.getElementById('summary-of-answer').style.display="none";
  }
  window.onload=on_load();
  
  document.getElementById('start-exam').onclick = function(){
  
  document.getElementById('upcoming-exam').style.display ="none";
  document.getElementById('exam-details').style.display = "block";
   
  }
  
  document.getElementById('back').onclick = function(){
  document.getElementById('upcoming-exam').style.display="block";
  document.getElementById('exam-details').style.display="none";
  }

  document.getElementById('next').onclick=function(){
    document.getElementById('exam-document').style.display="block";
    document.getElementById('exam-details').style.display="none";
  }

  // verification back and next btn
  document.getElementById('back-verification').onclick = function(){
    document.getElementById('exam-document').style.display="none";
    document.getElementById('exam-details').style.display="block";
    }
  
    document.getElementById('next-verification').onclick=function(){
      document.getElementById('exam-document').style.display="none";
      document.getElementById('exam-questions').style.display="block";
    }


    // question back and next btn
  document.getElementById('back-question').onclick = function(){
    document.getElementById('exam-questions').style.display="none";
    document.getElementById('exam-document').style.display="block";
    }
  
    document.getElementById('next-question').onclick=function(){
      document.getElementById('exam-questions').style.display="none";
      document.getElementById('exam-questions-2').style.display="block";
    }

    // question-2 back and next btn
  document.getElementById('back-question-2').onclick = function(){
    document.getElementById('exam-questions').style.display="block";
    document.getElementById('exam-questions-2').style.display="none";
    }
  
    document.getElementById('next-question-2').onclick=function(){
      document.getElementById('exam-questions-2').style.display="none";

      if(id=="exam-questions-3"){
        document.getElementById('exam-questions-3').style.display="block";
      }
      else{
        document.getElementById('question-summary').style.display="block";
      }
      
    }


    //summary question back and next btn
  document.getElementById('back-summary-question').onclick = function(){
    document.getElementById('exam-questions-2').style.display="block";
    document.getElementById('question-summary').style.display="none";
    }
  
    document.getElementById('next-summary-question').onclick=function(){
      document.getElementById('question-summary').style.display="none";
    document.getElementById('summary-of-answer').style.display="block";
    }

     //summary Answer back and next btn
  document.getElementById('back-summary-of-answer').onclick = function(){
    document.getElementById('question-summary').style.display="block";
    document.getElementById('summary-of-answer').style.display="none";
    }
  
    document.getElementById('next-summary-of-answer').onclick=function(){
      document.getElementById('exam-questions-2').style.display="none";
    document.getElementById('question-summary').style.display="block";
    }
    

    // on click submit btn
    // document.getElementById('submit').onclick=function(){
    //   document.getElementById('question-summary').style.display="block";
    //   document.getElementById('exam-questions').style.display="none";
    //   document.getElementById('exam-questions-2').style.display="none";
    // }


    document.getElementById('submit-2').onclick=function(){
      document.getElementById('question-summary').style.display="block";
      document.getElementById('exam-questions').style.display="none";
      document.getElementById('exam-questions-2').style.display="none";
    }

// view answer

document.getElementById('view-answer').onclick=function(){
  document.getElementById('summary-of-answer').style.display="block";
  document.getElementById('question-summary').style.display="none";

}


// chart js

new Chart(document.getElementById("bar-chart"), {
  type: 'bar',
  
  data: {
    labels: ["", "", "", "", ""],
    datasets: [
      {
        label: "",
        backgroundColor: ["#5cb85c","#f0ad4e","#d9534f","#0275d8"],
        data: [10,8,6,4]
      }
    ]
  },
  options: {
    display:true,
    width:'500',
    responsive:false,
      scales: {
          yAxes: [{
              ticks: {
                min: 2,
                 max: 10,
                 stepSize: 2,
                 callback: function(value){return value}
              }
          }],
        xAxes:[{
          ticks: {
            min: 2,
             max: 10,
             stepSize: 2,
             callback: function(value){return value}
          }
        }]
          
      },
      // legend:{
      //   position:"right",
      //  margin:{
      //   left:50,
      //   right:0
      //  }
        
      // },
      // layout:{
      //   padding:{
      //     top:50,
      //     left:20,
      //     right:20,
      //     bottom:10


      //   },
      //   width:500,
        
      // }
      
  }
});