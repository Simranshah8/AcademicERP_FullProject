

  on_load =()=>{
    document.getElementById('house-form').style.display="none";
    document.getElementById('house-dress-form').style.display="none";
    document.getElementById('student-type-form').style.display="none";
    document.getElementById('medium-form').style.display="none";
    
	
}
	window.onload=on_load();


 
  document.getElementById('add-house').onclick=function(){
      document.getElementById('house-name-section').style.display="none";
      document.getElementById('house-form').style.display="block";
      
  }

  document.getElementById('back-house-list').onclick = function(){
	document.getElementById('house-name-section').style.display="block";
	document.getElementById('house-form').style.display="none";
}


document.getElementById('add-house-dress').onclick=function(){
    document.getElementById('house-dress-section').style.display="none";
    document.getElementById('house-dress-form').style.display="block";
}

document.getElementById('back-housedress-list').onclick = function(){
  document.getElementById('house-dress-section').style.display="block";
  document.getElementById('house-dress-form').style.display="none";
}

// student-type js


document.getElementById('add-student').onclick=function(){
    document.getElementById('student-type-section').style.display="none";
    document.getElementById('student-type-form').style.display="block";
}

document.getElementById('back-student-list').onclick = function(){
  document.getElementById('student-type-section').style.display="block";
  document.getElementById('student-type-form').style.display="none";
}

// medium section js


document.getElementById('add-medium').onclick=function(){
    document.getElementById('medium-section').style.display="none";
    document.getElementById('medium-form').style.display="block";
}

document.getElementById('back-medium-list').onclick = function(){
  document.getElementById('medium-section').style.display="block";
  document.getElementById('medium-form').style.display="none";
}

