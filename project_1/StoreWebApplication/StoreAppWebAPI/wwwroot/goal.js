var numStores;
window.onload = function () {
    //console.log('Something');
    console.log(sessionStorage.getItem('user'));
    let user = JSON.parse(sessionStorage.getItem('user'));
    document.getElementById("place1").innerHTML = `Welcome ${user.fname} ${user.lname}!<br>Please select a store below!<br>`;
    //List the stores as buttons
    fetch("/storelist")
        .then(res => {
            if (!res.ok) {
                console.log('unable to retrieve list of stores.');
                throw new Error(`Network response was not ok (${res.status})`);
            }
            return res.json();
        })
        .then(res => {
            console.log(res);
            //print out the stores
            let stores = res;
            numStores = stores.length;
            console.log(stores.length);
            console.log(stores);
            let sL = document.getElementById("storeList");
            for (let i = 0; i < stores.length; i++) {
                sL.innerHTML += `<button type='button' class='butn' value='${stores[i].location}'>${stores[i].location}</button><br>`;
            }
            numStores = document.querySelectorAll(".butn").length;
            for (let j = 0; j < stores.length; j++) {
                document.querySelectorAll(".butn")[j].addEventListener("click", (e) => {
                    let stuff = document.querySelectorAll(".butn")[j].value;
                    //console.log(stuff);
                    sessionStorage.setItem('store', JSON.stringify(stuff));
                    console.log(sessionStorage.getItem('store'));
                    location.href = "store.html";
                });
            }
        })
        .catch(err => console.log(`There was an error ${err}`));
    //do a fetch for a list of orders for the current customer
    fetch(`/orderlist`)
        .then(res => {
            return res.json();
        })
        .then(res => {
            console.log(res);
            let orders = res;
            numOrders = orders.length;
            let oL = document.getElementById("orderList");
            let counter = 0;
            for (let k = 0; k < orders.length; k++) {
                console.log(orders[k]);
                if (orders[k].customer.username == user.username) {
                    counter++;
                    oL.innerHTML += `Order number: ${orders[k].orderID} Store location: ${orders[k].store.location} Total cost: $${orders[k].total}<br>`;
                }
            }
            if (counter == 0) {
                oL.innerHTML += `There are no orders for this user.`;
            }
        })
    
}
