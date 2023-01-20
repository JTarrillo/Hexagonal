const enviroment = "deveploment";
// const enviroment = "production";
const Auth = localStorage.getItem('AUTH');
const url_production = "";
const url_development = "http://localhost:9247/";
const URL_BASE = enviroment === "production" ? url_production : url_development;

const requestAll = async (method,params=[])=>{
    return await axios({
        headers: { 
            'Content-Type': 'application/json',
            //'Authorization': `Bearer ${Auth}`  || ''
        },
        method: 'POST',
        url: URL_BASE + method,
        params: null,
        data: params || []
    })
    .then(response=>{return response.data})
    .catch(err=>{
            if(err.response){
                return {message:err.response.data.message}
                //return {status:false,error_code:err.response.status,message:err.response}
            }else if(err.request){
                throw {status:false,error_code:404}
            }
            
        });
}