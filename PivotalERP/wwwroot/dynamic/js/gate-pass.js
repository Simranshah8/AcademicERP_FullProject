on_load =()=>{
 
    document.getElementById('student-form').style.display="none";
    document.getElementById('employee-form').style.display="none";
	
}
	window.onload=on_load();






document.getElementById('add-student').onclick = function(){
	document.getElementById('student-section').style.display="none";
	document.getElementById('student-form').style.display="block";
}
document.getElementById('sourceback-btn').onclick = function(){
	document.getElementById('student-form').style.display="none";
	document.getElementById('student-section').style.display="block";
}
// document.getElementById('visitor-book-link').onclick = function(){
// 	document.getElementById('student-form').style.display="none";
// 	document.getElementById('student-section').style.display="block";
// }

document.getElementById('add-employee').onclick = function(){
	document.getElementById('employee-section').style.display="none";
	document.getElementById('employee-form').style.display="block";
}
document.getElementById('setup-complainback-btn').onclick = function(){
	document.getElementById('employee-form').style.display="none";
	document.getElementById('employee-section').style.display="block";
}

