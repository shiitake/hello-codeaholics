import { createSlice, PayloadAction, createAsyncThunk } from "@reduxjs/toolkit"
import { Pharmacy, Response } from "../api/types"
import { apiService } from "../api/apiService";

interface PharmacyState {
    currentPharmacy: Pharmacy | null;
    pharmacies: Pharmacy[];
    status: "idle" | "loading" | "failed"
}

const initialState: PharmacyState = {
    currentPharmacy: null,
    pharmacies: [],
    status: "idle"
}

type PharmacyUpdate = Partial<Pharmacy>;

interface FetchPharmacyListArgs {
    pageNumber: number,
    postsPerPage: number,
}

export const fetchPharmacyList = createAsyncThunk<Pharmacy[], FetchPharmacyListArgs>(
  'pharmacy/fetchList',
  async ({ pageNumber, postsPerPage }) => {
    const pharmacyList = await apiService.getPharmacyList(pageNumber, postsPerPage);
    return pharmacyList;
  }
);

// Thunk action for updating pharmacy
export const updatePharmacy = createAsyncThunk<Pharmacy|Response, Pharmacy>(
  'pharmacy/updatePharmacy',
  async (pharmacy: Pharmacy) => {
    const updatedPharmacy = await apiService.updatePharmacy(pharmacy);
    return updatedPharmacy;
  }
);


export const pharmacySlice = createSlice({
    name: "pharmacy",
    initialState,
    reducers: {
        setCurrentPharmacy: (state, action: PayloadAction<Pharmacy|null>) => {
            state.currentPharmacy = action.payload;
        },
        updateCurrentPharmacy: (state, action: PayloadAction<PharmacyUpdate>) => {
            Object.entries(action.payload).forEach(([key, value]) => {
                if (state.currentPharmacy) {
                    (state.currentPharmacy as any)[key] = value;
                }
            });
        },
        setPharmacies: (state, action: PayloadAction<Pharmacy[]>) => {
            state.pharmacies = action.payload;
        },
        setStatus: (state, action: PayloadAction<"idle" | "loading" | "failed">) =>
        {
            state.status = action.payload;
        }

    },
    extraReducers: (builder) => {
        builder.addCase(fetchPharmacyList.fulfilled, (state, action: PayloadAction<Pharmacy[]>) => {
            state.pharmacies = action.payload;
        })
    }
});

export const { setCurrentPharmacy, setPharmacies, updateCurrentPharmacy, setStatus } = pharmacySlice.actions;

export default pharmacySlice.reducer;

