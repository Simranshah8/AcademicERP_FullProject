
on_load =()=>{
	document.getElementById('fee-item-form').style.display="none";
	document.getElementById('fee-item-group-form').style.display="none";
}
	window.onload=on_load();




// Fee Item
document.getElementById('add-fee-item').onclick = function(){
	document.getElementById('fee-tem-section').style.display="none";
	document.getElementById('fee-item-form').style.display="block";
}
document.getElementById('feeitem-back-btn').onclick = function(){
	document.getElementById('fee-item-form').style.display="none";
	document.getElementById('fee-tem-section').style.display="block";
}

// Fee Item Group
document.getElementById('add-fee-item-group').onclick = function(){
	document.getElementById('fee-item-group-section').style.display="none";
	document.getElementById('fee-item-group-form').style.display="block";
}
document.getElementById('fee-item-group-back-btn').onclick = function(){
	document.getElementById('fee-item-group-form').style.display="none";
	document.getElementById('fee-item-group-section').style.display="block";
}
