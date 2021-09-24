let cuUser = JSON.parse(sessionStorage.getItem('user'));
window.onload = function () {
    console.log(cuUser.username);
    console.log(cuUser.password);
    console.log(sessionStorage.getItem('user'));
    console.log(sessionStorage.getItem('store'));
    let curStore = JSON.parse(sessionStorage.getItem('store'));
    document.getElementById("title1").innerHTML = `Store Location: ${curStore}<br>`;
    fetch(`/product/${curStore}`)
        .then(res => {
            if (!res.ok) {
                console.log('unable to retrieve list of stores.');
                throw new Error(`Network response was not ok (${res.status})`);
            }
            return res.json();
        })
        .then(res => {
            console.log(res);
            let products = res;
            let numProducts = res.length;
            console.log(numProducts);
            console.log(products);
            let pL = document.getElementById("productList");
            for (let i = 0; i < numProducts; i++) {
                let tempQuantity;
                if (products[i].quantity > 5) {
                    tempQuantity = 5
                }
                else {
                    tempQuantity = products[i].quantity;
                }
                if (tempQuantity == 0) {
                    continue;
                }
                pL.innerHTML +=
                    `${products[i].product.name}<input id='${products[i].product.name}' name='${products[i].product.price}' class='prodselection' type='number' min=0 max=${tempQuantity}><br>`;
            }
            pL.innerHTML += `<button type='button' id='mybutton' class='butn'>Place Order</button>`;
            document.getElementById("mybutton").addEventListener("click", (e) => {
                let stuff = document.querySelectorAll(".prodselection");
                let tc = 0;
                for (let g = 0; g < stuff.length; g++) {
                    let curPrice = stuff[g].getAttribute("name");
                    let intPrice = parseInt(curPrice);
                    let totalCurPrice = stuff[g].value * intPrice;
                    tc += totalCurPrice;
                }
                if (tc > 0) {
                    fetch(`/makeorder/${cuUser.username}/${cuUser.password}/${curStore}/${tc}`, {
                        method: 'POST'
                    })
                .then(res => {
                    console.log(res);
                    location.href = "index.html";
                })
            }
                //console.log(stuff[0].value);
                //console.log(stuff[0].id);
                //location.href = "order.html";
            })
        })
    fetch(`/orderlist`)
        .then(res => {
            return res.json();
        })
        .then(res => {
            let orders = res;
            numOrders = orders.length;
            let oL = document.getElementById("orderList");
            let counter = 0;
            for (let k = 0; k < orders.length; k++) {
                console.log(orders[k]);
                if (orders[k].store.location == curStore) {
                    counter++;
                    oL.innerHTML += `Order number: ${orders[k].orderID}, Customer First Name: ${orders[k].customer.fname}, Total cost: $${orders[k].total}<br>`;
                }
            }
            if (counter == 0) {
                oL.innerHTML += `There are no orders for this location.`;
            }
        })
}

/*<input id=demoInput type=number min=100 max=110>
<button onclick="increment()">+</button>
<button onclick="decrement()">-</button>
<script>
   function increment() {
      document.getElementById('demoInput').stepUp();
   }
   function decrement() {
      document.getElementById('demoInput').stepDown();
   }
</script>
*/