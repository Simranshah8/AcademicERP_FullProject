
on_load =()=>{
	document.getElementById('postal-dispatch-form').style.display="none";
	document.getElementById('postal-received-form').style.display="none";
}
	window.onload=on_load();




// Postal Dispatch
document.getElementById('add-postal-dispatch').onclick = function(){
	document.getElementById('postdispatch-section').style.display="none";
	document.getElementById('postal-dispatch-form').style.display="block";
}
document.getElementById('postaldispatchback-btn').onclick = function(){
	document.getElementById('postal-dispatch-form').style.display="none";
	document.getElementById('postdispatch-section').style.display="block";
}

// Postal Received
document.getElementById('add-postal-received').onclick = function(){
	document.getElementById('postreceived-section').style.display="none";
	document.getElementById('postal-received-form').style.display="block";
}
document.getElementById('postalback-btn').onclick = function(){
	document.getElementById('postal-received-form').style.display="none";
	document.getElementById('postreceived-section').style.display="block";
}
