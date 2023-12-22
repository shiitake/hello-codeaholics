import React, { useEffect, useState } from 'react';
import { Table, Button, Modal, ButtonProps, Form, Input } from 'semantic-ui-react'
import { apiService } from '../api/apiService';
import { Pharmacy } from '../api/types';
import PharmacyModal from './pharmacymodal';
import './pharmacies.css';

const Pharmacies = () => {

    const [pageNumber, setPageNumber] = useState(1)
    const [postsPerPage, setPostsPerPage] = useState(5)
    const[isLoading, setIsLoading] = useState(false);
    const[isModalOpen, setIsModalOpen] = useState(false);
    const[pharmacyList, setPharmacyList] = useState<Pharmacy[]|null>(null);
    const[pharmacyData, setPharmacyData] = useState<Pharmacy|null>(null);
    
    useEffect(() => {
        const loadDataAsync = async () => {
            try {
                const apiData = await apiService.getPharmacyList(pageNumber, postsPerPage);
                setPharmacyList(apiData);
            }
            catch (error) {
                console.error("Failed to load data", error);
            }
        }
        loadDataAsync();
    }, []);    

    const handleEditClick = (event: React.MouseEvent<HTMLButtonElement, MouseEvent>, data: ButtonProps) => {
        event.preventDefault();
        if (pharmacyList)
        {
            setPharmacyData(pharmacyList[data.value]);
            console.log(pharmacyList[data.value]);
        }
        setIsModalOpen(true);
    }

    return (
        <>
        <div id="pharmacy-container">

        { pharmacyData !== null 
        ? <PharmacyModal pharmacyData={pharmacyData} />
        : ""        
    }
        <Table unstackable sortable selectable celled fixed className='pharmacy-table'>
            <Table.Header>
                <Table.Row>
                    <Table.HeaderCell>Name</Table.HeaderCell>
                    <Table.HeaderCell>Address</Table.HeaderCell>
                    <Table.HeaderCell>City</Table.HeaderCell>
                    <Table.HeaderCell>State</Table.HeaderCell>
                    <Table.HeaderCell>Zip Code</Table.HeaderCell>
                    <Table.HeaderCell>Fill Count</Table.HeaderCell>
                    <Table.HeaderCell></Table.HeaderCell>
                </Table.Row>
           </Table.Header>
           <Table.Body>
            {pharmacyList != null && pharmacyList.length > 0 ? pharmacyList.map((row: Pharmacy, index: number) => {
                return <Table.Row key={index} id="tr">
                    <Table.Cell>{row.name}</Table.Cell>
                    <Table.Cell>{row.address}</Table.Cell>
                    <Table.Cell>{row.city}</Table.Cell>
                    <Table.Cell>{row.state}</Table.Cell>
                    <Table.Cell>{row.zip}</Table.Cell>
                    <Table.Cell>{row.filledPrescriptionsCount}</Table.Cell>                    
                    <Table.Cell>
                        <Button
                        value={index}
                        onClick={(e: React.MouseEvent<HTMLButtonElement, MouseEvent>, data: ButtonProps) => handleEditClick(e, data)}>Edit</Button></Table.Cell>
                </Table.Row>
                }) : ""
            }
           </Table.Body>

        </Table>
        </div>
        </>

    )

}

export default Pharmacies