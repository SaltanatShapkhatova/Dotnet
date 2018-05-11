import React, { Component } from 'react';
import logo from './logo.svg';
import './App.css';

class App extends Component {
  constructor() {
      super();
      this.state = {
          customers: [{ fullName: '', depId: 0}],
          departments:[],
          details:[],


          products:[],
          inventory:[],
          pickedProducts:[],
          depProducts:[],
          fullName: '',

          depId: 0,
          isStarted: true,


          isCustomerPicked: false,
          isDepartmentPicked:true,
          isProductOpened:true,
          cName:'',




          dName:'',
          customerId:0,
          departmentId:0,
          startDate:0,
          endDate:0,
      };
  }


  componentWillMount() {
    fetch('http://localhost:59214/api/Customers')
      .then(Response => Response.json())
      .then((data) => {
          this.setState({
            customers: data,
          })
    });
      fetch('http://localhost:59214/api/Departments')
      .then(Response => Response.json())
      .then((data) => {
          this.setState({
            departments: data,
          })
    });
      fetch('http://localhost:59214/api/Products')
      .then(Response => Response.json())
      .then((data) => {
          this.setState({
            products: data,
          })
    });

      fetch('http://localhost:59214/api/Details')
      .then(Response => Response.json())
      .then((data) => {
          this.setState({
            details: data,
          })
    });

      fetch('http://localhost:59214/api/Inventories')
      .then(Response => Response.json())
      .then((data) => {
          this.setState({
            inventory: data,
          })
    });
        console.log(this.state.customers);
        console.log("here dep " );
        console.log(this.state.departments);
    }

    componentDidMount() {
        console.log("DidMount length");
        console.log(this.state.customers.length);
    }
    handleChangef(e) {
        this.setState({
            fullName: e.target.value
        });
    }
    handleChanged(e) {
        this.setState({
            depId: e.target.value
        });
    }
    startInventory(){
      let ok = this.state.isStarted;
      if(ok){
        ok = false;
      }
      else{
        ok = true;
      }
      this.setState({
            isStarted: ok 
        });
    }

    pickCutsomer(id,name){
      this.setState({
        startDate: Date.now(),
        isCustomerPicked:true,
        isDepartmentPicked:false,
        customerId:id,
        cName: name
      });
             //console.log(Date.now().format('HH:mm:ss'));
    }

    pickDepartment(id,name){
      let prs = this.state.depProducts;
      let dprs = prs.filter((item) => item.depId === id);
      this.setState({
        isDepartmentPicked:true,
        isProductOpened:false,
        departmentId:id,
        dName:name,
        depProducts: dprs,
      });
    }

    pickProducts(product){
      let prs = this.state.pickedProducts;
      let dprs = prs.filter((item) => item.productName === product.productName);
      if(dprs.length === 0){
        prs.push({id: product.id, depId: product.depId, productName: product.productName, quantity: product.quantity});
        this.setState({
          pickedProducts:prs
        });
      }
      else{
        alert("This product already picked");
      }

    }

    endInventory(){
      if(this.state.pickedProducts.length === 0){
        alert("You did not add products");
      }
      else{
        let det = this.state.details.slice();
        
        fetch("http://localhost:59214/api/Inventories", {
            method: 'POST',
            headers: {
                Accept: 'application/json',
                'Content-Type': 'application/json',
            },
            body: JSON.stringify({
                cId:this.state.customerId,
                startDate: Date.now,
                endDate: "2018-11-07",
                completed: true,
            }),
        });

        fetch('http://localhost:59214/api/Inventories')
      .then(Response => Response.json())
      .then((data) => {
          this.setState({
            inventory: data,
          })
    });

        let inv = this.state.inventory.slice();      
      
      var invId = 2;
      for(let i = 0;i < inv.length;i++){
        if(inv[i].cId === this.state.customerId){
          invId = inv[i].id;
          console.log("here run was" + inv[i].Id);
        }
        console.log("here run was not" + inv[i].cId);
      }

      for(let i = 0;i < this.state.pickedProducts.length;i++){
        fetch("http://localhost:59214/api/Details", {
            method: 'POST',
            headers: {
                Accept: 'application/json',
                'Content-Type': 'application/json',
            },
            body: JSON.stringify({                
                inventoryId: invId,
                pId: this.state.pickedProducts[i].id,
            }),
        });
        det.push({inventoryId: invId, pId: this.state.pickedProducts[i].id});

      }

       //console.log(Date.now().toString().getTime());
       this.setState({
        inventory:inv,
        details:det,
       });
      }
      this.setState({
          isStarted: true,
          isCustomerPicked: false,
          isDepartmentPicked:true,
          isProductOpened:true,
      });
    }

     render() {
        return <div>
          <button onClick={() => { this.startInventory() }} >Start Inventory</button>

              <div hidden = {this.state.isProductOpened}>
                <h4>Hey "{this.state.cName}" from {this.state.dName} department</h4>
                <h4>Start picking products</h4>
              </div>
          <div hidden = {this.state.isStarted} >
            <div className = "flx">
              <div hidden = {this.state.isCustomerPicked}>
                <h1>Customers</h1>
                <table className="table">
                    <thead>
                        <tr>
                            <th>FullName</th>
                            
                        </tr>
                    </thead>
                    <tbody>
                        {
                            this.state.customers.map((data, index) =>
                                <tr key={index}>
                                    <td>{data.fullName}</td>
                                    <td><button onClick={() => { this.pickCutsomer(data.id,data.fullName) } }>Pick Customer</button></td>                            
                                </tr>
                            )
                        }
                    </tbody>
                </table>
              </div>
              <div hidden = {this.state.isDepartmentPicked}>
                <h1>Departments</h1>
                <table className="table">
                    <thead>
                        <tr>
                            <th>FullName</th>
                            <th>Button</th>
                        </tr>
                    </thead>
                    <tbody>
                        {
                            this.state.departments.map((data, index) =>

                                <tr key={index}>

                                    <td>{data.depName}</td>                           
                                    <td><button onClick={() => { this.pickDepartment(data.id,data.depName) } }>Pick department</button></td>
                                </tr>
                            )
                        }
                    </tbody>
                </table>
              </div>
              <div hidden = {this.state.isProductOpened}>
                <h1>Products</h1>
                <table className="table">
                    <thead>
                        <tr>                            
                            <th>Product Name</th>
                            <th>DepId</th>
                            <th>Quantity</th>
                        </tr>
                    </thead>
                    <tbody>
                        {
                            this.state.depProducts.map((data, index) =>
                                <tr key={index}>                                    
                                    <td>{data.productName}</td>                           
                                    <td>{data.depId}</td>                           
                                    <td>{data.quantity}</td>                           
                                    <td><button onClick={() => { this.pickProducts(data)} }>Pick product</button></td>
                                </tr>
                            )
                        }
                    </tbody>
                </table>
                <div>
                  <h1>Picked products</h1>
                  <table className="table">
                      <thead>
                          <tr>                            
                              <th>Product Name</th>
                              <th>DepId</th>
                              <th>Quantity</th>
                          </tr>
                      </thead>
                      <tbody>
                          {
                              this.state.pickedProducts.map((data2, index) =>
                                  <tr key={index}>                                    
                                      <td>{data2.productName}</td>                           
                                      <td>{data2.depId}</td>                           
                                      <td>{data2.quantity}</td>                           
                                
                                  </tr>
                              )
                          }
                      </tbody>
                  </table>
                </div>
                <button onClick={()=>{ this.endInventory()}}>End Inventory</button>
              </div>            
            </div>
          </div>


          
              <div>
                <h1>Report</h1>
                <table className="table">
                    <thead>
                        <tr>
                            <th>InventoryId</th>
                            <th>Customer Name</th>
                            <th>Product Name</th>
                            <th>Start Date</th>
                            <th>End Date</th>
                            <th>completed</th>
                        </tr>
                    </thead>
                    <tbody>
                        {
                            this.state.report.map((data, index) =>
                                <tr key={index}>
                                  <td>{data.inventoryId }</td>  
                                  <td>{data.customerName}</td>                           
                                  <td>{data.productName}</td>                                  
                                  <td>{data.startDate}</td>                           
                                  <td>{data.endDate}</td>                        
                                  <td>"true"</td>                          
                                </tr>
                            )
                        }
                    </tbody>
                </table>
              </div>


              <div>
                <h1>Detail</h1>
                <table className="table">
                    <thead>
                        <tr>
                            <th>Inventory ID</th>
                            <th>product ID</th>
                        </tr>
                    </thead>
                    <tbody>
                        {
                            this.state.details.map((data, index) =>
                                <tr key={index}>
                                    <td>{data.inventoryId}</td>                           
                                    <td>{data.pId}</td></tr>
                            )
                        }
                    </tbody>
                </table>
              </div>

              <div>
                <h1>Inventory</h1>
                <table className="table">
                    <thead>
                        <tr>
                            <th>Inventory ID</th>
                            <th>Customer ID</th>
                            <th>Start Date</th>
                            <th>End Date</th>
                            <th>completed</th>
                        </tr>
                    </thead>
                    <tbody>
                        {
                            this.state.inventory.map((data, index) =>
                                <tr key={index}>
                                    <td>{data.id}</td>                           
                                    <td>{data.cId}</td>                           
                                    <td>{data.startDate}</td>                           
                                    <td>{data.endDate}</td>                           
                                    <td>"true"</td>                          
                                </tr>
                            )
                        }
                    </tbody>
                </table>
              </div>

        </div>;
    }

  
}

export default App;