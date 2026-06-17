on_load =()=>{
	
    document.getElementById('exam-type-form').style.display="none";
    document.getElementById('exam-type-group-form').style.display="none";
    document.getElementById('parent-exam-type-form').style.display="none";
   
  }
  window.onload=on_load();
  // exam-type section
 
  document.getElementById('add-exam-type').onclick=function(){
    document.getElementById('exam-type-section').style.display="none";
    document.getElementById('exam-type-form').style.display="block";
}

document.getElementById('back-exam-type').onclick = function(){
  document.getElementById('exam-type-section').style.display="block";
  document.getElementById('exam-type-form').style.display="none";
}

// exam type group section

document.getElementById('add-exam-type-group').onclick=function(){
    document.getElementById('exam-type-group-section').style.display="none";
    document.getElementById('exam-type-group-form').style.display="block";
}

document.getElementById('back-exam-type-group').onclick = function(){
  document.getElementById('exam-type-group-section').style.display="block";
  document.getElementById('exam-type-group-form').style.display="none";
}

//parent exam type group section


document.getElementById('add-parent-exam-type').onclick=function(){
    document.getElementById('parent-exam-type-section').style.display="none";
    document.getElementById('parent-exam-type-form').style.display="block";
}

document.getElementById('back-parent-exam-type').onclick = function(){
  document.getElementById('parent-exam-type-section').style.display="block";
  document.getElementById('parent-exam-type-form').style.display="none";
}