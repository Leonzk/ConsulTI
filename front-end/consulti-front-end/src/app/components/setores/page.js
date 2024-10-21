"use client"

import "./page.css"
import Link from "next/link";
import Cabecalho from "../cabecalho/page";
import { useEffect, useState } from "react";
import ReactModal from 'react-modal';

export default function Postagens() {

    
    const [errorItem, seterrorItem] = useState(false)
    const [stateEditar, setStateEditar] = useState(false);
    const [stateModal, setstateModal] = useState(false)
    const [stateVinc, setstateVinc] = useState([]);
    const [stateItens, setStateItens] = useState([]);
    const [stateItem, setStateItem] = useState({
        descricao: ""
    });
    const [stateItemId, setStateItemId] = useState("");
    const [stateTotal, setStateTotal] = useState(0);
    const [filtro, setFiltro] = useState("");
    useEffect(() => {
        fetch("http://localhost:5064/api/setor")
        .then(r => r.json())
        .then(r =>{
            setStateItens(r);
            setStateTotal(r.length);
            console.log(r);
        });
    }, []);

    async function handleFiltro(){
        if(filtro!=""){
            await fetch("http://localhost:5064/api/setor")
            .then(r => r.json())
            .then(r =>{
                var newArray = r.filter(function (item){
                    return item.razao_social.includes(filtro) ||
                            item.nome_fantasia.includes(filtro) ||
                            item.cnpj.includes(filtro) ||
                            item.id == filtro
                });
                console.log(newArray);
                setStateItens(newArray);
                console.log(r);
            });
        }
        else{
            await fetch("http://localhost:5064/api/setor")
            .then(r => r.json())
            .then(r =>{
                setStateItens(r);
                console.log(r);
            });
        }
    }

    async function handleExcluir(id){

        console.log(id);

        if(confirm("Deseja realmente excluir este setor?")){
            const requestOptions = {
                method: 'DELETE',
                body: "",
                headers: new Headers({
                'Content-Type': 'application/json',
                'Accept': 'application/json'
                }),
            };

            const response = await fetch(`http://localhost:5064/api/setor/${id}`, requestOptions);
            console.log(response);
        }
        else{

        }
    }

    const handleFiltrar = (event) => {
        const valor = event.target.value;
        setFiltro(valor);
    };

    function handleOpen(id){
        setstateModal(true);

        fetch(`http://localhost:5064/api/getvincsetores/${id}`)
        .then(r => r.json())
        .then(r =>{
            setstateVinc(r);
            console.log(r);
        });

    }

    function handleClose(){
        setstateModal(false)
    }

    
    async function handleEditarOpen(id){

        await fetch(`http://localhost:5064/api/setror/${id}`)
        .then(r => r.json())
            .then(r =>{
                setStateItem(r);
                setStateItemId(id);
                console.log(r);
            });
        setStateEditar(true)
    }

    function handleEditarClose(){
        seterrorItem(false)
        setStateEditar(false)
    }

    async function handleEditarSalvar(id){
        seterrorItem(false)
            const requestOptions = {
                method: 'POST',
                body: JSON.stringify(stateItem),
                headers: new Headers({
                'Content-Type': 'application/json',
                'Accept': 'application/json'
                }),
            };

            const response = await fetch(`http://localhost:5064/api/setor/atualizar/${id}`, requestOptions);
            console.log(response);


        setStateEditar(false)
        alert("Setor Alterado Com Sucesso")
    }

  return (
    <div>
      <Cabecalho></Cabecalho>
      
      <div className="principal">
        <h1 className="border-bottom pb-3 ml-5 mb-3 mr-5">Gerenciamento de Setores</h1>
        <div className="container w-100">
            <div className="flexcontainer w-100">
            <ReactModal isOpen={stateEditar}>
                    <div class="w-100 d-flex justify-content-between mb-3 w-50">
                        <h2>Atualizar Empresa</h2>
                        <button 
                            type="button"
                            className={"btn btn-outline-secondary mx-2 "}
                            onClick={() => handleEditarClose()}
                            >X Fechar</button>
                    </div>
                    <div class="form-body">
                        <label for="descricao" class="form-label">Descrição do Setor</label>
                        <div class="input-group mb-3">
                            <input value={stateItem.descricao} onChange={(e)=> setStateItem((stateItem) => ({...stateItem, descricao: e.target.value}))} type="text" class="form-control" id="descricao"/>
                        </div>
                    </div>
                    <div class="form-footer">
                        <button onClick={() => handleEditarSalvar(stateItemId)} type="button" class="btn btn-primary">Alterar</button>
                    </div>
                    {errorItem ?<p class="mt-3 text-danger">Formulário incorreto</p> : <></>}
                </ReactModal>
                <ReactModal isOpen={stateModal}
                 style={{
                    overlay: {
                      position: 'fixed',
                      top: 0,
                      left: 0,
                      right: 0,
                      bottom: 0,
                      backgroundColor: 'rgba(255, 255, 255, 0.75)'
                    },
                    content: {
                      position: 'absolute',
                      top: '10%',
                      left: '40px',
                      right: '40px',
                      bottom: '40px',
                      border: '1px solid #ccc',
                      background: '#fff',
                      overflow: 'auto',
                      WebkitOverflowScrolling: 'touch',
                      borderRadius: '4px',
                      outline: 'none',
                      padding: '20px'
                    }
                  }}
                >
                    <div class="w-100 d-flex justify-content-between mb-3 w-50">
                        <h2>Empresas desse setor</h2>
                        <button 
                            type="button"
                            className={"btn btn-outline-secondary mx-2 "}
                            onClick={() => handleClose()}
                            >X Fechar</button>
                    </div>
                    
                    <table className="table">
                    <thead>
                        <tr>
                            <th>Empresa Id</th>
                            <th>Razao Social</th>
                            <th>Nome Fantasia</th>
                            <th>CNPJ    </th>
                        </tr>
                    </thead>
                    <tbody className="">
                        {stateVinc.map(item => (
                            <tr>
                                <td>{item.empresa.id}</td>
                                <td>{item.empresa.razao_social}</td>
                                <td>{item.empresa.nome_fantasia}</td>
                                <td>{item.empresa.cnpj}</td>
                            </tr>
                        ))}
                    </tbody>
                </table>
                </ReactModal>
                <div class="input-group mb-3 w-100">
                    <input onChange={handleFiltrar} value={filtro} type="text" class="form-control" placeholder="Pesquisar"/>
                    <div class="">
                        <button onClick={handleFiltro}class="btn mx-2" type="button">Pesquisar</button>
                    </div>
                    <Link href={"./setores/novo"}><button type="button" class="btn btn-success ml-3">Novo</button></Link>
                </div>
                <div>Registros {stateItens.length}/{stateTotal}</div>
                <table className="table w-100">
                    <thead>
                        <tr>
                            <th>Setor Id</th>
                            <th>Descrição do Setor</th>
                        </tr>
                    </thead>
                    <tbody className="">
                        {stateItens.map(item => (
                            <tr>
                                <td>{item.id}</td>
                                <td>{item.descricao}</td>
                                <th className="d-flex flex-row flex-row-reverse">
                                    <button 
                                        type="button"
                                        className={"btn btn-outline-secondary mx-2 "}
                                        onClick={() => handleExcluir(item.id)}
                                        >🗑 Excluir</button>
                                    <button 
                                        type="button"
                                        className={"btn btn-outline-secondary mx-2 "}
                                        >🖉 Editar</button>
                                    
                                    <button 
                                        type="button"
                                        className={"btn btn-outline-secondary mx-2 "}
                                        onClick={() => handleOpen(item.id)}
                                        >▶ Ver Empresas</button>
                                </th>
                            </tr>
                        ))}


                    </tbody>
                </table>
                {stateItens.length==0 ?<h2>Sem Resultados</h2> : <></>}
            </div>
            
        </div>
      </div>
      
    </div>
  );
}
