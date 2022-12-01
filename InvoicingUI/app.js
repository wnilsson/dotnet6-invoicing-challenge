new Vue({    
    el:'#app',
    data(){
        return {
            title: 'Customer Account Health',
            invHealths:[],
            // Just using 2 fixed customerIds for this exercise
            custIndex:-1
        }
    },
    
    methods: {        
        fetchInvoiceHealth: function(custId) {
            // If we already have data for the custId, remove it from array and re-fetch
            var index;
            var customerExists = this.invHealths.some((x, i) => {
                index = i;
                return x.customerId == custId;
            })
            if (customerExists) this.invHealths.splice(index, 1);
            
            let url = `http://localhost/invoicing/api/invoicehealth/${custId}/take/10`;
            axios
                .get(url, { headers: { 'Access-Control-Allow-Origin': '*' }})
                .then(r => {
                this.invHealths.push(r.data)
                console.log(this.invHealths)
            })
        },
        getIndex: function(custId) {
            // Find the index of the custId 
            var index;
            var customerExists = this.invHealths.some((x, i) => {
                index = i;
                return x.customerId == custId;
            })
            if (customerExists) return index;
        },
        getHealthLabel(isHealthy) {
            if (isHealthy == true) 
                return "The account is healthy"; 
            else return "The account is not healthy"
        }        
    }
}) 

Vue.filter('toCurrency', function (value) {
    if (typeof value !== "number") {
        return value;
    }
    var formatter = new Intl.NumberFormat('en-AU', {
        style: 'currency',
        currency: 'AUD'
    });
    return formatter.format(value);
});

