
on_load =()=>{
	document.getElementById('add-calllog-form').style.display="none";
	document.getElementById('others').style.display="none";
	document.getElementById('employee').style.display="none";
}
	window.onload=on_load();

//Phonecall Log section
document.getElementById('add-calllog-enquiry').onclick = function(){
	document.getElementById('calllist-section').style.display="none";
	document.getElementById('add-calllog-form').style.display="block";
}
document.getElementById('callback-btn').onclick = function(){
	document.getElementById('add-calllog-form').style.display="none";
	document.getElementById('calllist-section').style.display="block";
}


// Radio Button Click
document.getElementById('inlineRadio1').onclick = function(){
	document.getElementById('others').style.display="none";
	document.getElementById('student').style.display="block";
	document.getElementById('employee').style.display="none";
	document.getElementById('class').style.display="block";
}
document.getElementById('inlineRadio2').onclick = function(){
	document.getElementById('student').style.display="none";
	document.getElementById('others').style.display="block";
	document.getElementById('employee').style.display="none";
	document.getElementById('class').style.display="none";
}

document.getElementById('inlineRadio3').onclick = function(){
	document.getElementById('student').style.display="none";
	document.getElementById('others').style.display="none";
	document.getElementById('employee').style.display="block";
	document.getElementById('class').style.display="none";
}