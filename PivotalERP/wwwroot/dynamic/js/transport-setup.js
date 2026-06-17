
on_load =()=>{
	document.getElementById('driver-form').style.display="none";
    document.getElementById('transport-route-form').style.display="none";
    document.getElementById('transport-point-form').style.display="none";		
}
	window.onload=on_load();
// Driver
document.getElementById('add-driver').onclick = function(){
	document.getElementById('driver-section').style.display="none";
	document.getElementById('driver-form').style.display="block";
}

document.getElementById('back-driver-list').onclick = function(){
	document.getElementById('driver-form').style.display="none";
	document.getElementById('driver-section').style.display="block";
}
// Transport Route
document.getElementById('add-transport-route').onclick = function(){
	document.getElementById('transport-route-section').style.display="none";
	document.getElementById('transport-route-form').style.display="block";
}

document.getElementById('back-transport-route').onclick = function(){
	document.getElementById('transport-route-section').style.display="block";
	document.getElementById('transport-route-form').style.display="none";
}

// Transport Point
document.getElementById('add-transport-point').onclick = function(){
	document.getElementById('transport-point-section').style.display="none";
	document.getElementById('transport-point-form').style.display="block";
}

document.getElementById('back-transport-point').onclick = function(){
	document.getElementById('transport-point-section').style.display="block";
	document.getElementById('transport-point-form').style.display="none";
}