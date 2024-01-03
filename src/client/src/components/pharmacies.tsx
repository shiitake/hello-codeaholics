import { useEffect } from 'react';
import { Table, Button, Loader, Segment, Dimmer} from 'semantic-ui-react'
import { Pharmacy } from '../api/types';
import PharmacyModal from './PharmacyModal'
import '../styles/pharmacies.css';
import { useSelector, useDispatch } from 'react-redux'
import { RootState, AppDispatch } from '../store/store'
import { setCurrentPharmacy, setStatus, fetchPharmacyList } from '../store/pharmacySlice';

const Pharmacies = () => {

    const dispatch: AppDispatch = useDispatch();
    const selectedPharmacy = useSelector((state: RootState) => state.pharmacies.currentPharmacy);
    const pharmacyList = useSelector((state: RootState) => state.pharmacies.pharmacies);
    const isLoading = useSelector((state: RootState) => state.pharmacies.status) == "loading";

    const loadDataAsync = async () => {
        try {
            dispatch(setStatus("loading"));
            await dispatch(fetchPharmacyList({pageNumber: 1, postsPerPage: 15})).unwrap();
        }
        catch (error) {
            dispatch(setStatus("failed"));
            console.error("Failed to load data", error);
        }
        finally {
            dispatch(setStatus("idle"))
        }
    } 
    useEffect(() => {
        loadDataAsync();
    }, [dispatch]);   
    
    const handleEdit = (pharmacy: Pharmacy) => {
        dispatch(setCurrentPharmacy(pharmacy));
    }

    return (
        <>
        <div id="pharmacy-container">
        <Segment className='pharmacy-segment'>
            <Dimmer active={isLoading}>
                <Loader size='tiny'>Loading</Loader>
            </Dimmer>
            <Table unstackable sortable selectable celled fixed className='pharmacy-table'>
                <Table.Header>
                    <Table.Row>
                        <Table.HeaderCell>Name</Table.HeaderCell>
                        <Table.HeaderCell>Address</Table.HeaderCell>
                        <Table.HeaderCell>City</Table.HeaderCell>
                        <Table.HeaderCell>State</Table.HeaderCell>
                        <Table.HeaderCell>Zip Code</Table.HeaderCell>
                        <Table.HeaderCell>Fill Count</Table.HeaderCell>
                        <Table.HeaderCell>Edit</Table.HeaderCell>
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
                            <Button icon='edit'
                            value={index}
                            onClick={() => handleEdit(row)} 
                            content='Edit'
                            />                            
                        </Table.Cell>
                    </Table.Row>
                    }) : null
                }
            </Table.Body>
            </Table>
            {selectedPharmacy && <PharmacyModal closeModal={() => { dispatch(setCurrentPharmacy(null)); loadDataAsync() }} />
            }
            </Segment>
        </div>
        </>

    )

}

export default Pharmacies