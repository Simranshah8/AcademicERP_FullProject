// Sheet Plan
on_load =()=>{
	document.getElementById('sheet-plan-form').style.display="none";
    document.getElementById('examshift-form').style.display="none";
	
}
	window.onload=on_load();

//Room Details
document.getElementById('add-room-details').onclick = function(){
	document.getElementById('add-room-section').style.display="none";
	document.getElementById('sheet-plan-form').style.display="block";
}
document.getElementById('addroomback-btn').onclick = function(){
	document.getElementById('sheet-plan-form').style.display="none";
	document.getElementById('add-room-section').style.display="block";
}


// Exam shift js part
document.getElementById('add-exam-shift').onclick = function(){
	document.getElementById('examshift-section').style.display="none";
	document.getElementById('examshift-form').style.display="block";
}
document.getElementById('examshift-back-btn').onclick = function(){
	document.getElementById('examshift-form').style.display="none";
	document.getElementById('examshift-section').style.display="block";
}
